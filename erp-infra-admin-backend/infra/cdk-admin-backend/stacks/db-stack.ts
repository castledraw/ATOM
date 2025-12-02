import * as cdk from 'aws-cdk-lib';
import { Construct } from 'constructs';
import * as rds from 'aws-cdk-lib/aws-rds';
import * as ec2 from 'aws-cdk-lib/aws-ec2';
import * as secrets from 'aws-cdk-lib/aws-secretsmanager';

interface DbStackProps extends cdk.StackProps {
  vpc: ec2.IVpc;
}

export class DbStack extends cdk.Stack {
  public readonly cluster: rds.ServerlessCluster;
  public readonly credentialsSecret: secrets.Secret;

  constructor(scope: Construct, id: string, props: DbStackProps) {
    super(scope, id, props);

    this.credentialsSecret = new secrets.Secret(this, 'DbCredentials', {
      description: 'Credentials for ERP Aurora PostgreSQL',
      generateSecretString: { secretStringTemplate: JSON.stringify({ username: 'erp_admin' }), generateStringKey: 'password' },
    });

    this.cluster = new rds.ServerlessCluster(this, 'ErpDb', {
      engine: rds.DatabaseClusterEngine.AURORA_POSTGRESQL,
      defaultDatabaseName: 'erp',
      vpc: props.vpc,
      enableDataApi: true,
      credentials: rds.Credentials.fromSecret(this.credentialsSecret),
      scaling: { autoPause: cdk.Duration.minutes(10) },
      removalPolicy: cdk.RemovalPolicy.DESTROY,
    });
  }
}
