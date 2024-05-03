import { A } from "@solidjs/router";
import ListingList from "../../components/ListingList";
import ListingsFilter from "../../components/ListingsFilter";

export default ({ appName, token, handleSetPage, tab, store }) => {
  return (
    <main>
      <div class="container">
        <div class="row g-3">

          <div class="col-lg-3">
            <div class="d-flex align-items-center d-lg-none">
              <button class="border-0 bg-transparent" type="button" data-bs-toggle="offcanvas" data-bs-target="#offcanvasSideNavbar" aria-controls="offcanvasSideNavbar">
                <span class="btn btn-primary"><i class="bi bi-sliders"></i></span>
                <span class="h6 mb-0 fw-bold d-lg-none ms-2">Kategoriler</span>
              </button>
            </div>
            <nav class="navbar navbar-expand-lg mx-0">
              <div class="offcanvas offcanvas-start" tabindex="-1" id="offcanvasSideNavbar">
                <div class="offcanvas-header">
                  <button type="button" class="btn-close text-reset ms-auto" data-bs-dismiss="offcanvas" aria-label="Close"></button>
                </div>

                <div class="offcanvas-body d-block px-2 px-lg-0">
                  <div class="card overflow-hidden">
                    <div class="card-body">
                      <p>Kategoriler</p>
                      <ul class="nav nav-link-secondary flex-column fw-bold gap-2">

                        <Suspense fallback="Loading categories...">
                          <For each={store.categories}>
                            {tag => (
                              <li class="nav-item">
                                <A class="nav-link" href={`#/?tab=${tag.id}`}>
                                  <i class="bi bi-chevron-right"></i>
                                  <span class="ms-2">{tag.name}</span>
                                </A>
                              </li>
                            )}
                          </For>
                        </Suspense>

                      </ul>
                    </div>
                  </div>
                </div>
              </div>
            </nav>

          </div>

          <div class="col-lg-9">

            <div class="card">
              <div class="card-body">
                <ListingsFilter />
              </div>
            </div>

            {/* <div class="clearfix mt-3">
              <div class="card">
                <div class="card-header d-sm-flex align-items-center justify-content-between border-0 pb-0">
                  <h1 class="h4 card-title">Süper Vitrin</h1>
                  <A class="btn btn-sm btn-primary-soft" href="javascript:;" data-bs-toggle="modal" data-bs-target="#modalCreateVideo"> <i class="bi bi-plus-lg pe-1"></i> İlanımı Vitrinde Göster</A>
                </div>
                <div class="card-body">
                  <div class="row g-3">

                    <div class="col-md-6">
                      <div class="card p-0 shadow-none border-0 position-relative">
                        <div class="position-relative">
                          <A href="listing-details-2">
                            <img class="rounded" src="https://www.patibul.com/uploads/ajax/2023/07/28/1-min-(18)-64C36B0510271-530_393.jpg" alt="" />
                          </A>
                          <div class="position-absolute bottom-0 start-0 p-3 d-flex w-100">
                            <span class="bg-dark bg-opacity-50 px-2 rounded text-white small">erkek</span>
                            <span class="bg-dark bg-opacity-50 px-2 rounded text-white small ms-auto">1 dk önce</span>
                          </div>
                        </div>
                        <div class="card-body px-0">
                          <h6 class="mb-0">
                            <A class="stretched-link" href="listing-details-2">Süper Dopingli Başlık</A>
                          </h6>
                        </div>
                      </div>
                    </div>

                    <div class="col-md-6">
                      <div class="card p-0 shadow-none border-0 position-relative">
                        <div class="position-relative">
                          <A href="listing-details-2">
                            <img class="rounded" src="https://www.patibul.com/uploads/ajax/2023/07/28/2-min-(23)-64C36B0AE69EE-530_393.jpg" alt="" />
                          </A>
                          <div class="position-absolute bottom-0 start-0 p-3 d-flex w-100">
                            <span class="bg-dark bg-opacity-50 px-2 rounded text-white small">erkek</span>
                            <span class="bg-dark bg-opacity-50 px-2 rounded text-white small ms-auto">1 dk önce</span>
                          </div>
                        </div>
                        <div class="card-body px-0">
                          <h6 class="mb-0">
                            <A class="stretched-link" href="listing-details-2">Altın Sarısı Golden NY11 British Shorthair</A>
                          </h6>
                        </div>
                      </div>
                    </div>

                    <div class="col-md-6">
                      <div class="card p-0 shadow-none border-0 position-relative">
                        <div class="position-relative">
                          <A href="listing-details-2">
                            <img class="rounded" src="https://www.patibul.com/uploads/ajax/2023/07/28/1-min-(18)-64C36B0510271-530_393.jpg" alt="" />
                          </A>
                          <div class="position-absolute bottom-0 start-0 p-3 d-flex w-100">
                            <span class="bg-dark bg-opacity-50 px-2 rounded text-white small">erkek</span>
                            <span class="bg-dark bg-opacity-50 px-2 rounded text-white small ms-auto">1 dk önce</span>
                          </div>
                        </div>
                        <div class="card-body px-0">
                          <h6 class="mb-0">
                            <A class="stretched-link" href="listing-details-2">Süper Dopingli Başlık</A>
                          </h6>
                        </div>
                      </div>
                    </div>

                    <div class="col-md-6">
                      <div class="card p-0 shadow-none border-0 position-relative">
                        <div class="position-relative">
                          <A href="listing-details-2">
                            <img class="rounded" src="https://www.patibul.com/uploads/ajax/2023/07/28/2-min-(23)-64C36B0AE69EE-530_393.jpg" alt="" />
                          </A>
                          <div class="position-absolute bottom-0 start-0 p-3 d-flex w-100">
                            <span class="bg-dark bg-opacity-50 px-2 rounded text-white small">erkek</span>
                            <span class="bg-dark bg-opacity-50 px-2 rounded text-white small ms-auto">1 dk önce</span>
                          </div>
                        </div>
                        <div class="card-body px-0">
                          <h6 class="mb-0">
                            <A class="stretched-link" href="listing-details-2">Altın Sarısı Golden NY11 British Shorthair</A>
                          </h6>
                        </div>
                      </div>
                    </div>

                  </div>
                </div>
              </div>
            </div> */}


            <div class="card mt-3">
              <div class="card-header d-sm-flex align-items-center justify-content-between border-0 pb-0">
                <h1 class="h4 card-title">Ana sayfa Vitrini</h1>
                <A class="btn btn-sm btn-primary-soft" href="javascript:;" data-bs-toggle="modal" data-bs-target="#modalCreateVideo"> <i class="bi bi-plus-lg pe-1"></i> İlanımı Vitrinde Göster</A>
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

        </div>
      </div>
    </main>
  );
};
