import Controller from '@ember/controller';
import { computed } from '@ember/object';
import { action } from '@ember/object';
import { inject as service } from '@ember/service';

export default class GenRecipeController extends Controller {
    @service restApi;
    @service router;

    isLoading = false;
    favoriteFlag = false; // Избранное: фильтр

    allProducts = []; // Избранное: фильтр
    favoriteProducts = []; // Избранное: фильтр
    products = [];

    searchStr = '';
    chosenProductPrimarykey = null;
    chosenProducts = [];

    // Default
    @action redirect(route) {
        this.router.transitionTo(route);
    }
    @action addChosenProduct(product) {
        var products = this.chosenProducts;
        products = products.concat(this.products.filter((item) => item.primarykey == product.primarykey));
        this.set('chosenProducts', products);
        console.log(this.get('chosenProducts'));
    }   
    @action removeFromChosenProducts(product) {
        var products = this.chosenProducts;
        products = products.filter((item) => item.primarykey != product.primarykey);
        this.set('chosenProducts', products);
        console.log(this.get('chosenProducts'));
    }
    @action generateRecipe() {
        var _this = this;
        var productKeys = Array.from(this.chosenProducts, p => p.primarykey).join("&");
        // var productKeys = this.chosenProducts.find((product) => product.isFavorite);
        _this.set('isLoading', true);
        this.restApi
            .sendPostRequest(
                'generate/recipe/products=' + productKeys
            )
            .then(
                function (recipe) {
                    _this.set('isLoading', false);
                    _this.router.transitionTo('recipes', {
                        queryParams: { chosenRecipePrimarykey: recipe.primarykey },
                    });
                },
                function () {
                    _this.set('isLoading', false);
                    alert('При генерации рецепта роизошла ошибка')
                }
            );
    }
    // Default

    // Избранное
    @action changeFavoriteFlag() {
        var isFavorite = !this.favoriteFlag;
        this.set('favoriteFlag', isFavorite);

        if (isFavorite) {
            this.set(
            'products',
            this.favoriteProducts.sort(function (a, b) {
                return a.name.localeCompare(b.name);
            }),
            );
        } else {
            this.set(
            'products',
            this.allProducts.sort(function (a, b) {
                return a.name.localeCompare(b.name);
            }),
            );
        }

        if (this.searchStr) {
            var searchProducts = this.products
            .filter((item) =>
                item.name.toLowerCase().includes(this.searchStr.toLowerCase()),
            )
            .sort(function (a, b) {
                return a.name.localeCompare(b.name);
            });
            this.set('products', searchProducts);
        }
    }
    // Избранное
    
    // Поиск
    @action changeSearchStr() {
        setTimeout(() => {
            var searchStr = this.searchStr;
            if (this.searchStr) {
                if (this.favoriteFlag) {
                    var searchProducts = this.favoriteProducts
                    .filter((item) =>
                        item.name.toLowerCase().includes(searchStr.toLowerCase()),
                    )
                    .sort(function (a, b) {
                        return a.name.localeCompare(b.name);
                    });
                    this.set('products', searchProducts);
                } else {
                    var searchProducts = this.allProducts
                    .filter((item) =>
                        item.name.toLowerCase().includes(searchStr.toLowerCase()),
                    )
                    .sort(function (a, b) {
                        return a.name.localeCompare(b.name);
                    });
                    this.set('products', searchProducts);
                }
            } else {
                if (this.favoriteFlag) {
                    this.set('products', this.favoriteProducts);
                } else {
                    this.set('products', this.allProducts);
                }
            }
        }, 250);
    }
    // Поиск
}
