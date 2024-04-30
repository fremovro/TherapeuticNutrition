import Controller from '@ember/controller';
import { computed } from '@ember/object';
import { action } from '@ember/object';
import { inject as service } from '@ember/service';

export default class DesktopController extends Controller {
  @service router;
  @service restApi;

  isLoading = false;
  pacient = null;

  @action redirect(route) {
    this.router.transitionTo(route);
  }

  @action redirectToAllergens(chosenAllergenPrimarykey) {
    this.router.transitionTo('allergens', {
      queryParams: { chosenAllergenPrimarykey: chosenAllergenPrimarykey },
    });
  }

  @action redirectToProducts(chosenProductPrimarykey) {
    this.router.transitionTo('products', {
      queryParams: { chosenProductPrimarykey: chosenProductPrimarykey },
    });
  }

  @action redirectToRecipes(chosenRecipePrimarykey) {
    this.router.transitionTo('recipes', {
      queryParams: { chosenRecipePrimarykey: chosenRecipePrimarykey },
    });
  }

  @action removeFromFavorite(type, primarykey) {
    var _this = this;

    this.restApi
      .sendPostRequest(
        'pacient/change/type=' +
          type +
          '&primarykey=' +
          primarykey +
          '&isFavorite=' +
          false,
      )
      .then(
        function (pacient) {
          _this.set('pacient', pacient);
        },
        function () {},
      );
  }
}
