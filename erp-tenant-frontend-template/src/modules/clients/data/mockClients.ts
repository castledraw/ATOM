export type Client = {
  id: string;
  companyCode: string;
  tenantCode: string;
  branchCode?: string;
  name: string;
  customerNumber: string;
  currency: string;
  paymentTerm: string;
  segment: string;
  creditLimit: number;
  creditBlocked: boolean;
  language?: string;
};

export const mockClients: Client[] = [
  {
    id: 'cust-1',
    companyCode: 'ACME-MX',
    tenantCode: 'ACME',
    branchCode: 'MX-HQ',
    name: 'Hotel Primavera',
    customerNumber: 'C-1001',
    currency: 'MXN',
    paymentTerm: 'NET30',
    segment: 'Enterprise',
    creditLimit: 50000,
    creditBlocked: false,
    language: 'es-MX',
  },
  {
    id: 'cust-2',
    companyCode: 'ACME-US',
    tenantCode: 'ACME',
    branchCode: 'US-AUS',
    name: 'Globex Retail',
    customerNumber: 'C-2001',
    currency: 'USD',
    paymentTerm: 'NET15',
    segment: 'Premium',
    creditLimit: 25000,
    creditBlocked: false,
    language: 'en-US',
  },
  {
    id: 'cust-3',
    companyCode: 'GLOB-AR',
    tenantCode: 'GLOB',
    branchCode: 'AR-CABA',
    name: 'Distribuciones Andinas',
    customerNumber: 'C-3001',
    currency: 'ARS',
    paymentTerm: 'CONTADO',
    segment: 'PyME',
    creditLimit: 8000,
    creditBlocked: true,
    language: 'es-AR',
  },
];
