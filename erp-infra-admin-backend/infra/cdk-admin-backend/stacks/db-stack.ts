import * as cdk from 'aws-cdk-lib';
import { Construct } from 'constructs';
import * as rds from 'aws-cdk-lib/aws-rds';
import * as ec2 from 'aws-cdk-lib/aws-ec2';

interface DbStackProps extends cdk.StackProps {
  vpc: ec2.IVpc;
}

export class DbStack extends cdk.Stack {
  public readonly cluster: rds.ServerlessCluster;

  constructor(scope: Construct, id: string, props: DbStackProps) {
    super(scope, id, props);

    this.cluster = new rds.ServerlessCluster(this, 'ErpDb', {
      engine: rds.DatabaseClusterEngine.AURORA_POSTGRESQL,
      defaultDatabaseName: 'erp',
      vpc: props.vpc,
      enableDataApi: true,
    });
  }
}
