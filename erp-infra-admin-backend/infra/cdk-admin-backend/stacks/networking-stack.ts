import * as cdk from 'aws-cdk-lib';
import { Construct } from 'constructs';
import * as ec2 from 'aws-cdk-lib/aws-ec2';
import * as elbv2 from 'aws-cdk-lib/aws-elasticloadbalancingv2';

interface NetworkingStackProps extends cdk.StackProps {
  vpc: ec2.IVpc;
}

export class NetworkingStack extends cdk.Stack {
  public readonly alb: elbv2.ApplicationLoadBalancer;

  constructor(scope: Construct, id: string, props: NetworkingStackProps) {
    super(scope, id, props);

    this.alb = new elbv2.ApplicationLoadBalancer(this, 'ErpAlb', {
      vpc: props.vpc,
      internetFacing: true,
    });

    this.alb.addListener('HttpListener', {
      port: 80,
      open: true,
    });
  }
}
