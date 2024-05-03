export default ({ appName, token, store }) => {
  return (
    <main>
      <div class="container">

        <div class="card">
          <div class="card-body">
            app name: {appName}<br />
            token: {token}
          </div>
        </div>

      </div>
    </main>
  );
};
