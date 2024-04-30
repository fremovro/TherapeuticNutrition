import Controller from '@ember/controller';
import { action } from '@ember/object';
import { inject as service } from '@ember/service';

export default class ProductsController extends Controller {
  @service restApi;
  @service router;
  queryParams = ['chosenProductPrimarykey'];

  isLoading = false;
  products = [];
  chosenProductPrimarykey = null;
  chosenProduct = null;

  @action redirect(route) {
    this.router.transitionTo(route);
  }

  @action changeChosenProduct(product) {
    this.set('chosenProduct', product);
  }

  @action makeFavorite(product) {
    this.set('chosenProduct.isFavorite', !product.isFavorite);

    this.restApi
      .sendPostRequest(
        'pacient/change/type=Product&primarykey=' +
          product.primarykey +
          '&isFavorite=' +
          product.isFavorite,
      )
      .then(
        function () {},
        function () {},
      );
  }
}
