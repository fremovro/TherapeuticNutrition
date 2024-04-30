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
  productImageUrl = 'https://www.ulfven.no/files/sculptor30/library/images/default-product-image.png';

  @action redirect(route) {
    this.router.transitionTo(route);
  }

  @action changeChosenProduct(product) {
    this.set('chosenProduct', product);
    this.send('setProductImageUrl');
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

  @action setProductImageUrl() {
    var _this = this;

    var chosenProduct = _this.get('chosenProduct');
    if (!chosenProduct) return null;

    this.restApi.sendGetRequest('https://localhost:7253/TherapeuticNutrition/get/image/relation=' + chosenProduct.primarykey)
    .then(
      function (productImageUrl) {
        _this.set('productImageUrl', productImageUrl);
      },
      function (reason) {
        _this.set('productImageUrl', 'https://www.ulfven.no/files/sculptor30/library/images/default-product-image.png');
      }
    );
  };
}
