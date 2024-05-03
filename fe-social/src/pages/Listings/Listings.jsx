import { A } from "@solidjs/router";
import ListingList from "../../components/ListingList";
import ListingsFilter from "../../components/ListingsFilter";

export default ({ appName, token, handleSetPage, tab, store }) => {
  return (
    <main>
      <div class="container">

        <div class="card">
          <div class="card-body">
            <ListingsFilter />
          </div>
        </div>

        <div class="card mt-3">
        <div class="card-header">
            <div class="d-flex">
              <div class="w-100">
                <strong>Sonuçlar</strong>
                <br />
                <small>Toplam <strong>{store.totalPagesCount}</strong> ilan bulundu.</small>
              </div>
              <div>
                <span class="float-end">
                  <select class="form-select" style="width: 200px;">
                    <option>Gelişmiş sıralama</option>
                    <option>Önce en yeniler</option>
                    <option>Önce en eskiler</option>
                  </select>
                </span>
              </div>
            </div>
          </div>
          <div class="card-body">

            <ListingList
              listings={Object.values(store.listings)}
              totalPagesCount={store.totalPagesCount}
              currentPage={store.page}
              onSetPage={handleSetPage}
            />

          </div>
        </div>



      </div>
    </main>
  );
};
