import { browser, by , element} from 'protractor';

export class PurchasesPage {
  navigateTo() {
    return browser.get('/purchases');
  }

  isTransactionListPresent() {
    return element(by.id('transactionsTable')).isPresent();
  }
}
