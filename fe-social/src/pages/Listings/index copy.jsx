import ListingsFilter from "../../components/ListingsFilter"
import { useStore } from "../../store"

export default () => {
  const [, { listings }] = useStore()
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
                <strong>Maltipoo</strong>
                <br />
                <small>toplam 123 ilan bulundu. Sıralama dopingi 5 tane ile sınırlı.</small>
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
            <div class="row row-cols-1 row-cols-sm-2 row-cols-md-3 row-cols-lg-4 row-cols-xl-5 g-3">

              <For each={listings()} fallback={<div>Yükleniyor...</div>}>
                {(listing) =>
                  <ListingItem listing={listing} />
                }
              </For>

            </div>


            <div class="border border-top"></div>
            <div class="text-center mt-3">
              <button class="btn btn-primary px-5" type="button">
                <span class="spinner-border spinner-border-sm me-2" role="status" aria-hidden="true"></span>
                Loading
              </button>
            </div>



          </div>
        </div>

      </div>
    </main>
  )
}