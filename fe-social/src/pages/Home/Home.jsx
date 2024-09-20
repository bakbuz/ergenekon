import { DefaultLayout } from "../../layouts/DefaultLayout";

export default ({ appName, token, store }) => {
  return (
    <DefaultLayout>
      <div class="container">

        <div class="card">
          <div class="card-body">
            app name: {appName}<br />
            token: {token}
          </div>
        </div>

      </div>
    </DefaultLayout>
  );
};
