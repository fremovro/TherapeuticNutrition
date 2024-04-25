import { module, test } from 'qunit';
import { setupTest } from 'therapeutic-nutrition/tests/helpers';

module('Unit | Service | rest-api', function (hooks) {
  setupTest(hooks);

  // TODO: Replace this with your real tests.
  test('it exists', function (assert) {
    let service = this.owner.lookup('service:rest-api');
    assert.ok(service);
  });
});
