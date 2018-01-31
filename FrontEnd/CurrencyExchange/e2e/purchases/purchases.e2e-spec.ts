import {PurchasesPage} from './purchases.po';

describe('Purchases page', () => {
  let page: PurchasesPage;

  beforeEach(() => {page = new PurchasesPage(); });

  it('should show purchase forms', () => {
    page.navigateTo();

    expect(page.areTransactionFormsPresent()).toBe(true);

  });
});
