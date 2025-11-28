import React from 'react';
import './styles.css';

const navItems = [
  { label: 'Tenants', href: '#/tenants' },
  { label: 'Catálogos', href: '#/catalogs' },
  { label: 'Despliegues', href: '#/deployments' },
];

export const AppLayout: React.FC<{ children: React.ReactNode }> = ({ children }) => {
  return (
    <div className="min-h-screen flex bg-slate-50">
      <aside className="w-64 bg-slate-900 text-white p-4 space-y-4">
        <div className="text-xl font-bold">ERP Admin</div>
        <p className="text-xs text-slate-300">SaaS multi-tenant • Cognito pendiente</p>
        <nav className="space-y-2">
          {navItems.map((item) => (
            <a key={item.label} className="block px-2 py-1 rounded hover:bg-slate-700" href={item.href}>
              {item.label}
            </a>
          ))}
        </nav>
      </aside>
      <main className="flex-1 p-6">
        <header className="flex flex-wrap items-center justify-between gap-4 mb-6">
          <div>
            <h1 className="text-2xl font-semibold text-slate-800">Portal Administrativo</h1>
            <p className="text-sm text-slate-500">Orquestador de tenants, empresas, catálogos y despliegues.</p>
          </div>
          <div className="flex gap-2 text-xs text-slate-600">
            <span className="rounded bg-emerald-100 px-3 py-1 font-semibold text-emerald-700">AWS Amplify + Cognito pendiente</span>
            <span className="rounded bg-amber-100 px-3 py-1 font-semibold text-amber-700">API Admin en modo mock</span>
          </div>
        </header>
        <section className="bg-white shadow-sm border rounded-lg p-4">{children}</section>
      </main>
    </div>
  );
};
