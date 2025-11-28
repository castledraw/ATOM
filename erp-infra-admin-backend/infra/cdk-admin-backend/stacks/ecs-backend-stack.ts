import * as cdk from 'aws-cdk-lib';
import { Construct } from 'constructs';
import * as ecs from 'aws-cdk-lib/aws-ecs';
import * as ec2 from 'aws-cdk-lib/aws-ec2';

interface EcsBackendStackProps extends cdk.StackProps {
  vpc: ec2.IVpc;
  clusterName: string;
}

export class EcsBackendStack extends cdk.Stack {
  public readonly cluster: ecs.Cluster;

  constructor(scope: Construct, id: string, props: EcsBackendStackProps) {
    super(scope, id, props);

    this.cluster = new ecs.Cluster(this, 'ErpCluster', {
      vpc: props.vpc,
      clusterName: props.clusterName,
    });
  }
}
