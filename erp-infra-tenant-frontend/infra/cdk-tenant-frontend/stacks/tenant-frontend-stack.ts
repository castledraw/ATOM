import * as cdk from 'aws-cdk-lib';
import { Construct } from 'constructs';
import * as s3 from 'aws-cdk-lib/aws-s3';
import * as cloudfront from 'aws-cdk-lib/aws-cloudfront';
import * as origins from 'aws-cdk-lib/aws-cloudfront-origins';

interface TenantFrontendStackProps extends cdk.StackProps {
  tenantId: string;
  subdomain: string;
  userPoolId: string;
  apiUrl: string;
}

export class TenantFrontendStack extends cdk.Stack {
  public readonly bucket: s3.Bucket;
  public readonly distribution: cloudfront.Distribution;

  constructor(scope: Construct, id: string, props: TenantFrontendStackProps) {
    super(scope, id, props);

    this.bucket = new s3.Bucket(this, 'TenantFrontendBucket', {
      websiteIndexDocument: 'index.html',
      blockPublicAccess: s3.BlockPublicAccess.BLOCK_ALL,
      versioned: true,
      enforceSSL: true,
    });

    const oai = new cloudfront.OriginAccessIdentity(this, 'TenantOAI');
    this.bucket.grantRead(oai);

    this.distribution = new cloudfront.Distribution(this, 'TenantDistribution', {
      defaultBehavior: {
        origin: new origins.S3Origin(this.bucket, { originAccessIdentity: oai }),
        viewerProtocolPolicy: cloudfront.ViewerProtocolPolicy.REDIRECT_TO_HTTPS,
      },
      domainNames: [props.subdomain],
      comment: `Tenant ${props.tenantId} connected to ${props.apiUrl} using user pool ${props.userPoolId}`,
      enableLogging: true,
      defaultRootObject: 'index.html',
    });
  }
}
