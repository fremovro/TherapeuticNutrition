import Controller from '@ember/controller';
import { action } from '@ember/object';
import { inject as service } from '@ember/service';

export default class ProductsController extends Controller {
  @service restApi;
  @service router;
  queryParams = ['chosenProductPrimarykey'];

  isLoading = false;
  favoriteFlag = false; // Избранное: фильтр

  allProducts = []; // Избранное: фильтр
  favoriteProducts = []; // Избранное: фильтр
  products = [];

  searchStr = '';
  chosenProductPrimarykey = null;
  chosenProduct = null;
  productImageUrl = 'https://www.ulfven.no/files/sculptor30/library/images/default-product-image.png';

  // Default
  @action redirect(route) {
    this.router.transitionTo(route);
  };
  @action changeChosenProduct(product) {
    this.set('chosenProduct', product);
    this.send('setProductImageUrl');
  };
  // Default

  // Избранное
  @action makeFavorite(product) {
    this.set('chosenProduct.isFavorite', !product.isFavorite);

    // Избранное: фильтр
    let favoriteProducts = this.get('favoriteProducts');
    if (product.isFavorite) {
      favoriteProducts.push(product);
      this.set('favoriteProducts', favoriteProducts);
    }
    else {
      favoriteProducts = favoriteProducts.filter(item => item !== product);
      this.set('favoriteProducts', favoriteProducts);
    }
    if (this.get('favoriteFlag')) {
      this.set('products', favoriteProducts.sort(function(a,b){
        return a.name.localeCompare(b.name);
      }));
    }

    this.restApi
      .sendPostRequest(
        'pacient/change/type=Product&primarykey=' +
          product.primarykey +
          '&isFavorite=' +
          product.isFavorite,
      )
      .then(
        function (pacient) {
          console.log(pacient);
        },
        function (error) {
          console.log(error);
        }
      );
  };
  @action changeFavoriteFlag() {
    var isFavorite = !this.get('favoriteFlag');
    this.set('favoriteFlag', isFavorite);

    if (isFavorite) {
      this.set('products', this.get('favoriteProducts').sort(function(a,b){
        return a.name.localeCompare(b.name);
      }));
    }
    else {
      this.set('products', this.get('allProducts').sort(function(a,b){
        return a.name.localeCompare(b.name);
      }));
    }

    if (this.get('searchStr')) {
      var searchProducts = this.get('products')
        .filter(item => item.name.toLowerCase().includes(this.get('searchStr').toLowerCase()))
        .sort(function(a,b) {return a.name.localeCompare(b.name); });
        this.set('products', searchProducts);
    }

    this.send('changeChosenProduct', this.get('products')[0]);
  };
  // Избранное

  // Поиск
  @action changeSearchStr() {
    setTimeout(() => {
      var searchStr = this.get('searchStr');
      if (this.get('searchStr')) {
        if (this.get('favoriteFlag')) {
          var searchProducts = this.get('favoriteProducts')
            .filter(item => item.name.toLowerCase().includes(searchStr.toLowerCase()))
            .sort(function(a,b) {return a.name.localeCompare(b.name); });
            this.set('products', searchProducts);
        }
        else {
          var searchProducts = this.get('allProducts')
            .filter(item => item.name.toLowerCase().includes(searchStr.toLowerCase()))
            .sort(function(a,b) {return a.name.localeCompare(b.name); });
            this.set('products', searchProducts);
        }
      }
      else {
        if (this.get('favoriteFlag')) {
          this.set('products', this.get('favoriteProducts'));
        }
        else {
          this.set('products', this.get('allProducts'));
        }
      }
    }, 250);
  };
  // Поиск

  // Image
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
  // Image
}