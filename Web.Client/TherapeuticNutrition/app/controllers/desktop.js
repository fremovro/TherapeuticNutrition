import Controller from '@ember/controller';
import { action } from '@ember/object';
import { inject as service } from '@ember/service';

export default class DesktopController extends Controller {
  @service router;

  pacient = null;

  @action redirect(route) {
    this.router.transitionTo(route);
  }
}
