import { useStore } from "../store";
import ListingPreview from "./ListingPreview";
import ListingItem from "./ListingItem";

export default props => {
  const [{ token }, { unmakeFavorite, makeFavorite }] = useStore(),
    handleClickFavorite = (listing, e) => {
      e.preventDefault();
      listing.favorited ? unmakeFavorite(slug) : makeFavorite(slug);
    },
    handlePage = (v, e) => {
      e.preventDefault();
      props.onSetPage(v);
      setTimeout(() => window.scrollTo(0, 0), 200);
    };
  return (
    <Suspense fallback={<div class="listing-preview">Loading listings...</div>}>

      <div class="row row-cols-1 row-cols-sm-2 row-cols-md-3 row-cols-lg-4 row-cols-xl-5 g-3">
        <For each={props.listings} fallback={<div>Yükleniyor...</div>}>
          {(listing) =>
            <ListingItem listing={listing} />
          }
        </For>
      </div>

      {/* <For
        each={props.listings}
        fallback={<div class="listing-preview">No listings are here... yet.</div>}
      >
        {listing => (
          <ListingPreview listing={listing} token={token} onClickFavorite={handleClickFavorite} />
        )}
      </For> */}

      <Show when={props.totalPagesCount > 1}>
        <nav>
          <ul class="pagination">
            <For each={[...Array(props.totalPagesCount).keys()]}>
              {v => (
                <li
                  class="page-item"
                  classList={{ active: props.currentPage === v }}
                  onClick={[handlePage, v]}
                >
                  <a class="page-link" href="" textContent={v + 1} />
                </li>
              )}
            </For>
          </ul>
        </nav>
      </Show>
    </Suspense>
  );
};
