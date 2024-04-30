import Route from '@ember/routing/route';
import { action } from '@ember/object';
import { inject as service } from '@ember/service';

export default class ProductsRoute extends Route {
  @service restApi;

  @action didTransition() {
    var _this = this;

    _this.controller.set('isLoading', true);
    this.restApi
      .sendGetRequest(
        'https://localhost:7253/TherapeuticNutrition/get/products',
      )
      .then(
        function (products) {
          setTimeout(() => {
            _this.controller.set('isLoading', false);
            _this.controller.set('products', products);

            if (_this.controller.get('chosenProductPrimarykey') != null) {
              _this.controller.set(
                'chosenProduct',
                products.find((products) => {
                  return (
                    products.primarykey ==
                    _this.controller.get('chosenProductPrimarykey')
                  );
                }),
              );
            }

            if (_this.controller.get('chosenProduct') == null) {
              _this.controller.set('chosenProduct', products[0]);
            }

            console.log(products);
          }, 500);
        },
        function (reason) {
          setTimeout(() => {
            _this.controller.set('isLoading', false);
            _this.controller.send('redirect', 'desktop');
          }, 500);
        },
      );
  }
}
