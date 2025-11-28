export const mockTenants = [
  {
    id: 't-001',
    code: 'ACME',
    name: 'Acme Corp Suite',
    domain: 'acme.example.com',
    defaultLanguage: 'es-MX',
    companies: 2,
    status: 'active',
  },
  {
    id: 't-002',
    code: 'GLOB',
    name: 'Globex Group',
    domain: 'globex.example.com',
    defaultLanguage: 'en-US',
    companies: 1,
    status: 'pending',
  },
];

export const mockCompanies = [
  {
    id: 'c-001',
    tenantId: 't-001',
    code: 'ACME-MX',
    legalName: 'Acme Corp MX',
    defaultCurrency: 'MXN',
    defaultLanguage: 'es-MX',
    enabled: true,
  },
  {
    id: 'c-002',
    tenantId: 't-001',
    code: 'ACME-US',
    legalName: 'Acme Corp USA',
    defaultCurrency: 'USD',
    defaultLanguage: 'en-US',
    enabled: true,
  },
  {
    id: 'c-003',
    tenantId: 't-002',
    code: 'GLOB-AR',
    legalName: 'Globex Argentina',
    defaultCurrency: 'ARS',
    defaultLanguage: 'es-AR',
    enabled: false,
  },
];

export const mockBranches = [
  {
    id: 'b-001',
    companyId: 'c-001',
    code: 'MX-HQ',
    name: 'CDMX Centro',
    phone: '+52 55 1234 5678',
    email: 'cdmx@acme.com',
    address: 'Av. Reforma 101, CDMX',
    enabled: true,
  },
  {
    id: 'b-002',
    companyId: 'c-001',
    code: 'MTY-SAT',
    name: 'Monterrey Satélite',
    phone: '+52 81 1111 2222',
    email: 'mty@acme.com',
    address: 'Blvd. Sendero 55, MTY',
    enabled: true,
  },
  {
    id: 'b-003',
    companyId: 'c-002',
    code: 'US-AUS',
    name: 'Austin HQ',
    phone: '+1 737 555 0101',
    email: 'austin@acme.com',
    address: 'Congress Ave 200, Austin',
    enabled: true,
  },
  {
    id: 'b-004',
    companyId: 'c-003',
    code: 'AR-CABA',
    name: 'Buenos Aires Centro',
    phone: '+54 11 5555 0101',
    email: 'caba@globex.com',
    address: 'Av. Libertador 4040, CABA',
    enabled: false,
  },
];

export const mockCustomers = [
  {
    id: 'cust-1',
    companyId: 'c-001',
    number: 'C-1001',
    displayName: 'Hotel Primavera',
    currency: 'MXN',
    paymentTerm: 'NET30',
    segment: 'Enterprise',
    creditLimit: 50000,
    creditBlocked: false,
  },
  {
    id: 'cust-2',
    companyId: 'c-002',
    number: 'C-2001',
    displayName: 'Globex Retail',
    currency: 'USD',
    paymentTerm: 'NET15',
    segment: 'Premium',
    creditLimit: 25000,
    creditBlocked: false,
    branchCode: 'US-AUS',
  },
  {
    id: 'cust-3',
    companyId: 'c-003',
    number: 'C-3001',
    displayName: 'Distribuciones Andinas',
    currency: 'ARS',
    paymentTerm: 'CONTADO',
    segment: 'PyME',
    creditLimit: 8000,
    creditBlocked: true,
    branchCode: 'AR-CABA',
  },
];

export const mockIndustries = [
  { id: 'ind-1', code: 'HOTEL', name: 'Hospitalidad', description: 'Hoteles, resorts y centros vacacionales' },
  { id: 'ind-2', code: 'RETAIL', name: 'Retail', description: 'Tiendas físicas y online' },
  { id: 'ind-3', code: 'DISTRIB', name: 'Distribución', description: 'Mayoristas y logística' },
];

export const mockSegments = [
  { id: 'seg-1', companyId: 'c-001', code: 'ENT', name: 'Enterprise', description: 'Grandes cuentas con procesos complejos' },
  { id: 'seg-2', companyId: 'c-002', code: 'PREM', name: 'Premium', description: 'Clientes estratégicos' },
  { id: 'seg-3', companyId: 'c-003', code: 'PYME', name: 'PyME', description: 'Pequeñas y medianas empresas' },
];
