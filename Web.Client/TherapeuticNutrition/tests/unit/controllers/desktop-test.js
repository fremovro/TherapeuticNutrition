import { module, test } from 'qunit';
import { setupTest } from 'therapeutic-nutrition/tests/helpers';

module('Unit | Controller | desktop', function (hooks) {
  setupTest(hooks);

  // TODO: Replace this with your real tests.
  test('it exists', function (assert) {
    let controller = this.owner.lookup('controller:desktop');
    assert.ok(controller);
  });
});
