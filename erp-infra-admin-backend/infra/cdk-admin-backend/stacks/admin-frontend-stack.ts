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
      publicReadAccess: false,
    });

    this.distribution = new cloudfront.Distribution(this, 'AdminDistribution', {
      defaultBehavior: { origin: new origins.S3Origin(this.bucket) },
      domainNames: [props.distributionDomain],
    });
  }
}
