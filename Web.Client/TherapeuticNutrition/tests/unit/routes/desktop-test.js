import { module, test } from 'qunit';
import { setupTest } from 'therapeutic-nutrition/tests/helpers';

module('Unit | Route | desktop', function (hooks) {
  setupTest(hooks);

  test('it exists', function (assert) {
    let route = this.owner.lookup('route:desktop');
    assert.ok(route);
  });
});
