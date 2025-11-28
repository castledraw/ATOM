import * as cdk from 'aws-cdk-lib';
import { Construct } from 'constructs';
import * as cognito from 'aws-cdk-lib/aws-cognito';

interface CognitoCoreStackProps extends cdk.StackProps {}

export class CognitoCoreStack extends cdk.Stack {
  public readonly userPool: cognito.UserPool;

  constructor(scope: Construct, id: string, props?: CognitoCoreStackProps) {
    super(scope, id, props);

    this.userPool = new cognito.UserPool(this, 'AdminUserPool', {
      selfSignUpEnabled: false,
      userPoolName: 'erp-admin-users',
    });

    this.userPool.addClient('AdminAppClient', {
      userPoolClientName: 'erp-admin-portal',
      authFlows: { userPassword: true },
      generateSecret: false,
    });
  }
}
