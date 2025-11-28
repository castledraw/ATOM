import React from 'react';
import { currencies, languages, paymentTerms } from '../catalogs/mockCatalogs';
import { mockBranches, mockCompanies, mockCustomers, mockIndustries, mockSegments, mockTenants } from './data/mockTenants';

export const TenantsPage: React.FC = () => {
  return (
    <div className="space-y-6">
      <header className="space-y-1">
        <h2 className="text-xl font-semibold text-slate-800">Tenants ERP</h2>
        <p className="text-sm text-slate-500">
          Vista mock ERP-grade: tenants SaaS con multi-empresa, idiomas, moneda y términos de pago listos para conectar a Cognito + backend.
        </p>
      </header>

      <section className="grid gap-4 md:grid-cols-2">
        {mockTenants.map((tenant) => (
          <article key={tenant.id} className="rounded-lg border border-slate-200 bg-white p-4 shadow-sm">
            <div className="flex items-start justify-between">
              <div>
                <h3 className="text-lg font-semibold text-slate-800">{tenant.name}</h3>
                <p className="text-xs text-slate-500">Código: {tenant.code}</p>
                <p className="text-xs text-slate-500">Dominio: {tenant.domain}</p>
              </div>
              <span className="rounded-full bg-emerald-50 px-3 py-1 text-xs font-semibold uppercase text-emerald-700">
                {tenant.status}
              </span>
            </div>
            <dl className="mt-3 grid grid-cols-2 gap-2 text-sm text-slate-700">
              <div>
                <dt className="text-xs uppercase text-slate-500">Empresas</dt>
                <dd className="font-semibold">{tenant.companies}</dd>
              </div>
              <div>
                <dt className="text-xs uppercase text-slate-500">Idioma por defecto</dt>
                <dd className="font-semibold">{tenant.defaultLanguage}</dd>
              </div>
            </dl>
            <div className="mt-3">
              <h4 className="text-sm font-semibold text-slate-800">Empresas</h4>
              <ul className="mt-2 space-y-1 text-sm text-slate-700">
                {mockCompanies
                  .filter((c) => c.tenantId === tenant.id)
                  .map((company) => (
                    <li key={company.id} className="space-y-1 rounded border border-slate-100 px-2 py-2">
                      <div className="flex items-center justify-between">
                        <div>
                          <p className="font-semibold">{company.legalName}</p>
                          <p className="text-xs text-slate-500">{company.code} • {company.defaultLanguage} • {company.defaultCurrency}</p>
                        </div>
                        <span
                          className={`text-xs font-semibold ${company.enabled ? 'text-emerald-700' : 'text-amber-700'}`}
                        >
                          {company.enabled ? 'Activa' : 'Suspendida'}
                        </span>
                      </div>
                      <div className="rounded bg-slate-50 p-2">
                        <p className="text-xs font-semibold text-slate-600">Sucursales</p>
                        <ul className="mt-1 space-y-1">
                          {mockBranches
                            .filter((branch) => branch.companyId === company.id)
                            .map((branch) => (
                              <li key={branch.id} className="text-xs text-slate-600">
                                <span className="font-semibold">{branch.code}</span> · {branch.name} · {branch.address}
                              </li>
                            ))}
                        </ul>
                      </div>
                    </li>
                  ))}
              </ul>
            </div>
          </article>
        ))}
      </section>

      <section className="grid gap-4 md:grid-cols-3">
        <div className="rounded-lg border border-slate-200 bg-white p-4 shadow-sm">
          <h4 className="text-sm font-semibold text-slate-800">Catálogo: Términos de pago</h4>
          <ul className="mt-2 space-y-1 text-sm text-slate-700">
            {paymentTerms.map((term) => (
              <li key={term.code} className="flex items-center justify-between rounded px-2 py-1 hover:bg-slate-50">
                <span className="font-semibold">{term.name}</span>
                <span className="text-xs text-slate-500">{term.code} · {term.days} días</span>
              </li>
            ))}
          </ul>
        </div>
        <div className="rounded-lg border border-slate-200 bg-white p-4 shadow-sm">
          <h4 className="text-sm font-semibold text-slate-800">Catálogo: Monedas</h4>
          <ul className="mt-2 space-y-1 text-sm text-slate-700">
            {currencies.map((currency) => (
              <li key={currency.code} className="flex items-center justify-between rounded px-2 py-1 hover:bg-slate-50">
                <span className="font-semibold">{currency.name}</span>
                <span className="text-xs text-slate-500">{currency.code} • {currency.symbol}</span>
              </li>
            ))}
          </ul>
        </div>
        <div className="rounded-lg border border-slate-200 bg-white p-4 shadow-sm">
          <h4 className="text-sm font-semibold text-slate-800">Clientes destacados</h4>
          <ul className="mt-2 space-y-1 text-sm text-slate-700">
            {mockCustomers.slice(0, 3).map((customer) => (
              <li key={customer.id} className="rounded px-2 py-1 hover:bg-slate-50">
                <p className="font-semibold">{customer.displayName}</p>
                <p className="text-xs text-slate-500">
                  {customer.number} • {customer.currency} • {customer.paymentTerm} • {customer.segment}
                </p>
              </li>
            ))}
          </ul>
        </div>
        <div className="rounded-lg border border-slate-200 bg-white p-4 shadow-sm">
          <h4 className="text-sm font-semibold text-slate-800">Idiomas y localización</h4>
          <ul className="mt-2 space-y-1 text-sm text-slate-700">
            {languages.map((lang) => (
              <li key={lang.code} className="flex items-center justify-between rounded px-2 py-1 hover:bg-slate-50">
                <span className="font-semibold">{lang.name}</span>
                <span className="text-xs text-slate-500">{lang.code} {lang.isDefault ? '• default' : ''}</span>
              </li>
            ))}
          </ul>
        </div>
        <div className="rounded-lg border border-slate-200 bg-white p-4 shadow-sm">
          <h4 className="text-sm font-semibold text-slate-800">Industrias</h4>
          <ul className="mt-2 space-y-1 text-sm text-slate-700">
            {mockIndustries.map((industry) => (
              <li key={industry.id} className="rounded px-2 py-1 hover:bg-slate-50">
                <p className="font-semibold">{industry.name}</p>
                <p className="text-xs text-slate-500">{industry.code} • {industry.description}</p>
              </li>
            ))}
          </ul>
        </div>
        <div className="rounded-lg border border-slate-200 bg-white p-4 shadow-sm">
          <h4 className="text-sm font-semibold text-slate-800">Segmentos por empresa</h4>
          <ul className="mt-2 space-y-1 text-sm text-slate-700">
            {mockSegments.map((segment) => (
              <li key={segment.id} className="rounded px-2 py-1 hover:bg-slate-50">
                <p className="font-semibold">{segment.name}</p>
                <p className="text-xs text-slate-500">{segment.code} • {segment.description}</p>
              </li>
            ))}
          </ul>
        </div>
      </section>
    </div>
  );
};
