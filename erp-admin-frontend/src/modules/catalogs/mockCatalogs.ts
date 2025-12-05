export const currencies = [
  { code: 'USD', name: 'Dólar estadounidense', symbol: '$', decimalPlaces: 2 },
  { code: 'MXN', name: 'Peso mexicano', symbol: '$', decimalPlaces: 2 },
  { code: 'ARS', name: 'Peso argentino', symbol: '$', decimalPlaces: 2 },
];

export const paymentTerms = [
  { code: 'NET30', name: 'Pago a 30 días', days: 30 },
  { code: 'NET15', name: 'Pago a 15 días', days: 15 },
  { code: 'CONTADO', name: 'Contado', days: 0 },
];

export const languages = [
  { code: 'es-MX', name: 'Español (México)', isDefault: true },
  { code: 'en-US', name: 'English (US)', isDefault: false },
  { code: 'es-AR', name: 'Español (Argentina)', isDefault: false },
];
