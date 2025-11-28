import React from 'react';

const mockTenants = [
  { id: 't-001', name: 'Acme Corp', domain: 'acme.example.com', status: 'active' },
  { id: 't-002', name: 'Globex', domain: 'globex.example.com', status: 'pending' },
];

export const TenantsPage: React.FC = () => {
  return (
    <div className="space-y-4">
      <div>
        <h2 className="text-xl font-semibold text-slate-800">Tenants</h2>
        <p className="text-sm text-slate-500">Listado mock; conectar con backend y Cognito via Amplify en siguientes iteraciones.</p>
      </div>
      <div className="overflow-x-auto">
        <table className="min-w-full text-sm">
          <thead>
            <tr className="bg-slate-100 text-slate-700">
              <th className="px-3 py-2 text-left">ID</th>
              <th className="px-3 py-2 text-left">Nombre</th>
              <th className="px-3 py-2 text-left">Dominio</th>
              <th className="px-3 py-2 text-left">Estado</th>
            </tr>
          </thead>
          <tbody className="divide-y">
            {mockTenants.map((tenant) => (
              <tr key={tenant.id} className="hover:bg-slate-50">
                <td className="px-3 py-2 font-mono">{tenant.id}</td>
                <td className="px-3 py-2">{tenant.name}</td>
                <td className="px-3 py-2">{tenant.domain}</td>
                <td className="px-3 py-2">{tenant.status}</td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
};
