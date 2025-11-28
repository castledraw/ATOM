import React from 'react';

const mockClients = [
  {
    id: 'c-001',
    actor: 'Industrias Aurora S.A.',
    company: 'Acme Latam',
    customerNumber: 'ACME-00045',
    email: 'contacto@aurora.com',
    phone: '+505 555 0101',
    currency: 'USD',
    paymentTerm: 'NET30',
    segment: 'Enterprise',
    creditLimit: '50,000',
    status: 'Cliente',
  },
  {
    id: 'c-002',
    actor: 'Servicios Brisa',
    company: 'Acme Latam',
    customerNumber: 'ACME-00046',
    email: 'hola@brisa.io',
    phone: '+505 555 0202',
    currency: 'EUR',
    paymentTerm: 'CONTADO',
    segment: 'PYME',
    creditLimit: '5,000',
    status: 'Prospecto',
  },
];

export const ClientsListPage: React.FC = () => {
  return (
    <div className="space-y-3">
      <h2 className="text-xl font-semibold text-slate-800">Clientes</h2>
      <p className="text-sm text-slate-500">
        Tabla mock ERP-grade: clientes como rol de actor por empresa, con moneda, términos de pago, segmento y límites de
        crédito. Se conectará a APIs multi-tenant y Cognito por tenant.
      </p>
      <div className="overflow-x-auto">
        <table className="min-w-full text-sm">
          <thead>
            <tr className="bg-slate-100 text-slate-700">
              <th className="px-3 py-2 text-left">ID</th>
              <th className="px-3 py-2 text-left">Actor</th>
              <th className="px-3 py-2 text-left">Empresa</th>
              <th className="px-3 py-2 text-left">N° Cliente</th>
              <th className="px-3 py-2 text-left">Email</th>
              <th className="px-3 py-2 text-left">Teléfono</th>
              <th className="px-3 py-2 text-left">Moneda</th>
              <th className="px-3 py-2 text-left">Término de pago</th>
              <th className="px-3 py-2 text-left">Segmento</th>
              <th className="px-3 py-2 text-left">Límite de crédito</th>
              <th className="px-3 py-2 text-left">Estado</th>
            </tr>
          </thead>
          <tbody className="divide-y">
            {mockClients.map((client) => (
              <tr key={client.id} className="hover:bg-slate-50">
                <td className="px-3 py-2 font-mono">{client.id}</td>
                <td className="px-3 py-2">{client.actor}</td>
                <td className="px-3 py-2">{client.company}</td>
                <td className="px-3 py-2 font-mono text-xs">{client.customerNumber}</td>
                <td className="px-3 py-2">{client.email}</td>
                <td className="px-3 py-2">{client.phone}</td>
                <td className="px-3 py-2">{client.currency}</td>
                <td className="px-3 py-2">{client.paymentTerm}</td>
                <td className="px-3 py-2">{client.segment}</td>
                <td className="px-3 py-2">{client.creditLimit}</td>
                <td className="px-3 py-2">{client.status}</td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
};
