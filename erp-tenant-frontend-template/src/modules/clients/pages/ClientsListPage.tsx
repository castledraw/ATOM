import React from 'react';

const mockClients = [
  { id: 'c-001', name: 'Cliente Uno', email: 'uno@example.com', phone: '+1 555 0101' },
  { id: 'c-002', name: 'Cliente Dos', email: 'dos@example.com', phone: '+1 555 0202' },
];

export const ClientsListPage: React.FC = () => {
  return (
    <div className="space-y-3">
      <h2 className="text-xl font-semibold text-slate-800">Clientes</h2>
      <p className="text-sm text-slate-500">Tabla mock; se conectará a APIs multi-tenant y Cognito por tenant.</p>
      <div className="overflow-x-auto">
        <table className="min-w-full text-sm">
          <thead>
            <tr className="bg-slate-100 text-slate-700">
              <th className="px-3 py-2 text-left">ID</th>
              <th className="px-3 py-2 text-left">Nombre</th>
              <th className="px-3 py-2 text-left">Email</th>
              <th className="px-3 py-2 text-left">Teléfono</th>
            </tr>
          </thead>
          <tbody className="divide-y">
            {mockClients.map((client) => (
              <tr key={client.id} className="hover:bg-slate-50">
                <td className="px-3 py-2 font-mono">{client.id}</td>
                <td className="px-3 py-2">{client.name}</td>
                <td className="px-3 py-2">{client.email}</td>
                <td className="px-3 py-2">{client.phone}</td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
};
