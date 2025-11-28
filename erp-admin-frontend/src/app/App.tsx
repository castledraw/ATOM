import React from 'react';
import { AppLayout } from './AppLayout';
import { TenantsPage } from '../modules/tenants/TenantsPage';

export const App: React.FC = () => {
  return (
    <AppLayout>
      <TenantsPage />
    </AppLayout>
  );
};
