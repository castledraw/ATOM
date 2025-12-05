import React from 'react';
import { ClientsListPage } from '../modules/clients/pages/ClientsListPage';

export const App: React.FC = () => {
  return (
    <div className="min-h-screen bg-slate-50 p-6">
      <header className="mb-4">
        <h1 className="text-2xl font-semibold text-slate-800">ERP Tenant Portal</h1>
        <p className="text-sm text-slate-500">Cognito por tenant + APIs multi-tenant se configurarán vía CDK/Amplify.</p>
      </header>
      <div className="bg-white shadow-sm border rounded-lg p-4">
        <ClientsListPage />
      </div>
    </div>
  );
};
