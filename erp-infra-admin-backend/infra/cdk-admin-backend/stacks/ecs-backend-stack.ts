import * as cdk from 'aws-cdk-lib';
import { Construct } from 'constructs';
import * as ecs from 'aws-cdk-lib/aws-ecs';
import * as ec2 from 'aws-cdk-lib/aws-ec2';
import * as elbv2 from 'aws-cdk-lib/aws-elasticloadbalancingv2';
import * as ecsPatterns from 'aws-cdk-lib/aws-ecs-patterns';

interface EcsBackendStackProps extends cdk.StackProps {
  vpc: ec2.IVpc;
  clusterName: string;
  listener?: elbv2.ApplicationListener;
}

export class EcsBackendStack extends cdk.Stack {
  public readonly cluster: ecs.Cluster;
  public readonly adminService: ecsPatterns.ApplicationLoadBalancedFargateService;

  constructor(scope: Construct, id: string, props: EcsBackendStackProps) {
    super(scope, id, props);

    this.cluster = new ecs.Cluster(this, 'ErpCluster', {
      vpc: props.vpc,
      clusterName: props.clusterName,
      containerInsights: true,
    });

    this.adminService = new ecsPatterns.ApplicationLoadBalancedFargateService(this, 'AdminApiService', {
      cluster: this.cluster,
      cpu: 256,
      memoryLimitMiB: 512,
      desiredCount: 1,
      taskImageOptions: {
        image: ecs.ContainerImage.fromRegistry('public.ecr.aws/docker/library/nginx:latest'),
        containerPort: 80,
        environment: { ASPNETCORE_ENVIRONMENT: 'Development' },
      },
      publicLoadBalancer: false,
      assignPublicIp: false,
    });

    if (props.listener) {
      props.listener.addTargets('AdminApiTargets', {
        port: 80,
        targets: [this.adminService.service],
        healthCheck: { path: '/health', healthyHttpCodes: '200-399' },
      });
    }
  }
}
