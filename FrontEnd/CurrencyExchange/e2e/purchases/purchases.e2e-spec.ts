import {LoginPage} from './purchases.po';

describe('Login page', () => {
  let page: LoginPage;

  beforeEach(() => {page = new LoginPage(); });

  it('should login user', () => {
    page.navigateTo();

    page.setLogin('tmx');
    page.setPassword('asd');
    page.login();
    expect(page.isTransactionListPresent()).toBe(true);

  });
});
