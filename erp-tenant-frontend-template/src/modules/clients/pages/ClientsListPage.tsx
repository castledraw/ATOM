import React, { useMemo, useState } from 'react';
import { mockClients } from '../data/mockClients';

export const ClientsListPage: React.FC = () => {
  const [tenantFilter, setTenantFilter] = useState('');
  const [segmentFilter, setSegmentFilter] = useState('');

  const filtered = useMemo(() => {
    return mockClients.filter((client) => {
      const matchesTenant = tenantFilter ? client.tenantCode === tenantFilter : true;
      const matchesSegment = segmentFilter ? client.segment === segmentFilter : true;
      return matchesTenant && matchesSegment;
    });
  }, [tenantFilter, segmentFilter]);

  return (
    <div className="space-y-4">
      <div className="flex flex-col gap-3 md:flex-row md:items-center md:justify-between">
        <div>
          <h2 className="text-xl font-semibold text-slate-800">Clientes</h2>
          <p className="text-sm text-slate-500">
            Plantilla multi-tenant para listar clientes con moneda, términos de pago y segmentación listos para Amplify + API.
          </p>
        </div>
        <div className="flex gap-2">
          <select
            className="rounded border border-slate-200 px-3 py-2 text-sm"
            value={tenantFilter}
            onChange={(e) => setTenantFilter(e.target.value)}
          >
            <option value="">Todos los tenants</option>
            <option value="ACME">ACME</option>
            <option value="GLOB">GLOB</option>
          </select>
          <select
            className="rounded border border-slate-200 px-3 py-2 text-sm"
            value={segmentFilter}
            onChange={(e) => setSegmentFilter(e.target.value)}
          >
            <option value="">Todos los segmentos</option>
            <option value="Enterprise">Enterprise</option>
            <option value="Premium">Premium</option>
            <option value="PyME">PyME</option>
          </select>
        </div>
      </div>

      <div className="overflow-x-auto">
        <table className="min-w-full text-sm">
          <thead>
            <tr className="bg-slate-100 text-slate-700">
              <th className="px-3 py-2 text-left">Tenant</th>
              <th className="px-3 py-2 text-left">Empresa</th>
              <th className="px-3 py-2 text-left">Sucursal</th>
              <th className="px-3 py-2 text-left">Cliente</th>
              <th className="px-3 py-2 text-left">Moneda</th>
              <th className="px-3 py-2 text-left">Término de pago</th>
              <th className="px-3 py-2 text-left">Segmento</th>
              <th className="px-3 py-2 text-left">Idioma</th>
              <th className="px-3 py-2 text-left">Crédito</th>
            </tr>
          </thead>
          <tbody className="divide-y">
            {filtered.map((client) => (
              <tr key={client.id} className="hover:bg-slate-50">
                <td className="px-3 py-2 font-mono text-xs">{client.tenantCode}</td>
                <td className="px-3 py-2 font-mono text-xs">{client.companyCode}</td>
                <td className="px-3 py-2 font-mono text-xs">{client.branchCode ?? '—'}</td>
                <td className="px-3 py-2">
                  <div className="font-semibold">{client.name}</div>
                  <div className="text-xs text-slate-500">{client.customerNumber}</div>
                </td>
                <td className="px-3 py-2">{client.currency}</td>
                <td className="px-3 py-2">{client.paymentTerm}</td>
                <td className="px-3 py-2">{client.segment}</td>
                <td className="px-3 py-2">{client.language ?? '—'}</td>
                <td className="px-3 py-2">
                  <span className={`rounded px-2 py-1 text-xs font-semibold ${client.creditBlocked ? 'bg-amber-100 text-amber-700' : 'bg-emerald-100 text-emerald-700'}`}>
                    {client.creditBlocked ? 'Bloqueado' : 'Activo'} · {client.creditLimit.toLocaleString()}
                  </span>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
};
