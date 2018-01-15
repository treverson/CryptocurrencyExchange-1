import { browser, by , element} from 'protractor';

export class LoginPage {
  navigateTo() {
    return browser.get('/login');
  }

  setLogin(login: string) {
    element(by.id('login')).sendKeys(login);
  }

  setPassword(password: string) {
    element(by.id('password')).sendKeys(password);
  }

  login() {
    element(by.id('loginButton')).click();
  }

  isTransactionListPresent() {
    return element(by.id('transactionsTable')).isPresent();
  }
}
