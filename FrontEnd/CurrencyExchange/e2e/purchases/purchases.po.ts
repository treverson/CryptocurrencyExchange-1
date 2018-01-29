import { browser, by , element} from 'protractor';

export class PurchasesPage {
  navigateTo() {
    return browser.get('/purchases');
  }

  areTransactionFormsPresent() {

    return element(by.id('cryptoForms') && by.id('plnForms')).isPresent();
  }
}
