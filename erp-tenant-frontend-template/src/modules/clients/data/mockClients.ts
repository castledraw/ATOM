export type Client = {
  id: string;
  companyCode: string;
  tenantCode: string;
  name: string;
  customerNumber: string;
  currency: string;
  paymentTerm: string;
  segment: string;
  creditLimit: number;
  creditBlocked: boolean;
};

export const mockClients: Client[] = [
  {
    id: 'cust-1',
    companyCode: 'ACME-MX',
    tenantCode: 'ACME',
    name: 'Hotel Primavera',
    customerNumber: 'C-1001',
    currency: 'MXN',
    paymentTerm: 'NET30',
    segment: 'Enterprise',
    creditLimit: 50000,
    creditBlocked: false,
  },
  {
    id: 'cust-2',
    companyCode: 'ACME-US',
    tenantCode: 'ACME',
    name: 'Globex Retail',
    customerNumber: 'C-2001',
    currency: 'USD',
    paymentTerm: 'NET15',
    segment: 'Premium',
    creditLimit: 25000,
    creditBlocked: false,
  },
  {
    id: 'cust-3',
    companyCode: 'GLOB-AR',
    tenantCode: 'GLOB',
    name: 'Distribuciones Andinas',
    customerNumber: 'C-3001',
    currency: 'ARS',
    paymentTerm: 'CONTADO',
    segment: 'PyME',
    creditLimit: 8000,
    creditBlocked: true,
  },
];
