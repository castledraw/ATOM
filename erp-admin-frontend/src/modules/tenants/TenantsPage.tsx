import React from 'react';

const mockTenants = [
  {
    id: 't-001',
    code: 'ACME',
    name: 'Acme Corp',
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

export const TenantsPage: React.FC = () => {
  return (
    <div className="space-y-4">
      <div>
        <h2 className="text-xl font-semibold text-slate-800">Tenants</h2>
        <p className="text-sm text-slate-500">
          Vista mock ERP-grade: tenants SaaS con multi-empresa, idiomas y dominios. Conectar a backend + Cognito via Amplify en
          próximas iteraciones.
        </p>
      </div>
      <div className="overflow-x-auto">
        <table className="min-w-full text-sm">
          <thead>
            <tr className="bg-slate-100 text-slate-700">
              <th className="px-3 py-2 text-left">ID</th>
              <th className="px-3 py-2 text-left">Código</th>
              <th className="px-3 py-2 text-left">Nombre</th>
              <th className="px-3 py-2 text-left">Dominio</th>
              <th className="px-3 py-2 text-left">Idioma por defecto</th>
              <th className="px-3 py-2 text-left">Empresas</th>
              <th className="px-3 py-2 text-left">Estado</th>
            </tr>
          </thead>
          <tbody className="divide-y">
            {mockTenants.map((tenant) => (
              <tr key={tenant.id} className="hover:bg-slate-50">
                <td className="px-3 py-2 font-mono">{tenant.id}</td>
                <td className="px-3 py-2 font-mono text-xs">{tenant.code}</td>
                <td className="px-3 py-2">{tenant.name}</td>
                <td className="px-3 py-2">{tenant.domain}</td>
                <td className="px-3 py-2">{tenant.defaultLanguage}</td>
                <td className="px-3 py-2">{tenant.companies}</td>
                <td className="px-3 py-2">{tenant.status}</td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
};
