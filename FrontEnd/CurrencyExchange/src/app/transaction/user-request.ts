import {User} from '../security/user';
import {OwnedCurrency} from './owned-currency';

export class UserRequest {
  OwnedCurrencies: OwnedCurrency[];
  User: User;
}
