import * as cdk from 'aws-cdk-lib';
import { Construct } from 'constructs';
import * as s3 from 'aws-cdk-lib/aws-s3';
import * as cloudfront from 'aws-cdk-lib/aws-cloudfront';
import * as origins from 'aws-cdk-lib/aws-cloudfront-origins';

interface AdminFrontendStackProps extends cdk.StackProps {
  distributionDomain: string;
}

export class AdminFrontendStack extends cdk.Stack {
  public readonly bucket: s3.Bucket;
  public readonly distribution: cloudfront.Distribution;

  constructor(scope: Construct, id: string, props: AdminFrontendStackProps) {
    super(scope, id, props);

    this.bucket = new s3.Bucket(this, 'AdminFrontendBucket', {
      websiteIndexDocument: 'index.html',
      blockPublicAccess: s3.BlockPublicAccess.BLOCK_ALL,
      versioned: true,
      enforceSSL: true,
    });

    const originAccessIdentity = new cloudfront.OriginAccessIdentity(this, 'AdminOAI');
    this.bucket.grantRead(originAccessIdentity);

    this.distribution = new cloudfront.Distribution(this, 'AdminDistribution', {
      defaultBehavior: { origin: new origins.S3Origin(this.bucket, { originAccessIdentity }) },
      domainNames: [props.distributionDomain],
      defaultRootObject: 'index.html',
      enableLogging: true,
      comment: 'Admin portal CloudFront + S3 static hosting',
    });
  }
}
