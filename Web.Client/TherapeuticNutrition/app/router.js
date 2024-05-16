import EmberRouter from '@ember/routing/router';
import config from 'therapeutic-nutrition/config/environment';

export default class Router extends EmberRouter {
  location = config.locationType;
  rootURL = config.rootURL;
}

Router.map(function () {
  this.route('desktop');
  this.route('authorization');
  this.route('allergens');
  this.route('products');
  this.route('recipes');
  this.route('gen-recipe');
});
