import React from 'react';
import './styles.css';

const navItems = [
  { label: 'Tenants', href: '#/tenants' },
  { label: 'Suscripciones', href: '#/subscriptions' },
  { label: 'Planes', href: '#/plans' },
  { label: 'Despliegues', href: '#/deployments' },
];

export const AppLayout: React.FC<{ children: React.ReactNode }> = ({ children }) => {
  return (
    <div className="min-h-screen flex bg-slate-50">
      <aside className="w-64 bg-slate-900 text-white p-4 space-y-4">
        <div className="text-xl font-bold">ERP Admin</div>
        <nav className="space-y-2">
          {navItems.map((item) => (
            <a key={item.label} className="block px-2 py-1 rounded hover:bg-slate-700" href={item.href}>
              {item.label}
            </a>
          ))}
        </nav>
      </aside>
      <main className="flex-1 p-6">
        <header className="flex justify-between items-center mb-6">
          <h1 className="text-2xl font-semibold text-slate-800">Portal Administrativo</h1>
          <div className="text-sm text-slate-500">Auth + Amplify pendientes</div>
        </header>
        <section className="bg-white shadow-sm border rounded-lg p-4">{children}</section>
      </main>
    </div>
  );
};
