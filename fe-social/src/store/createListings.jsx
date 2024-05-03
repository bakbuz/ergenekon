import { createResource, createSignal } from "solid-js";

const LIMIT = 10;

export default function createListings(agent, actions, state, setState) {
  //const [searchParams] = useSearchParams();
  const [listingSource, setListingSource] = createSignal();
  const [listings] = createResource(
    listingSource,
    (args, { value }) => {
      console.info(args)
      console.info(value)
      if (args[0] === "listings") {
        return $req2(args[1]).then(({ listings, totalCount }) => {
          queueMicrotask(() => setState({ totalPagesCount: Math.ceil(totalCount / LIMIT) }));
          return listings.reduce((memo, listing) => {
            memo[listing.id] = listing;
            return memo;
          }, {});
        });
      }
      const listing = state.listings[args[1]];
      if (listing) return value;
      return agent.Listings.get(args[1]).then((listing) => ({ ...value, [args[1]]: listing }));
    },
    { initialValue: {} }
  );

  function $req(predicate) {
    if (predicate.myFeed) return agent.Listings.feed(state.page, LIMIT);
    if (predicate.favoritedBy)
      return agent.Listings.favoritedBy(predicate.favoritedBy, state.page, LIMIT);
    if (predicate.tag) return agent.Listings.byTag(predicate.tag, state.page, LIMIT);
    if (predicate.author) return agent.Listings.byAuthor(predicate.author, state.page, LIMIT);
    return agent.Listings.all(state.page, LIMIT, predicate);
  }

  function $req2(predicate) {
    //return agent.Listings.listings(predicate.breedId, predicate.ownerId, predicate.provinceId, predicate.districtId, predicate.neighborhoodId, state.page,LIMIT);
    console.log("predicate", predicate)
    //console.log("searchParams", searchParams)
    const filter = location.search;
    return agent.Listings.getListings(filter, state.page, LIMIT)
  }

  Object.assign(actions, {
    setPage: (page) => setState({ page }),
    loadListings(predicate) {
      setListingSource(["listings", predicate]);
    },
    loadListing(id) {
      setListingSource(["listing", id]);
    },
    async makeFavorite(id) {
      const listing = state.listings[id];
      if (listing && !listing.favorited) {
        setState("listings", id, (s) => ({
          favorited: true,
          favoritesCount: s.favoritesCount + 1
        }));
        try {
          await agent.Listings.favorite(id);
        } catch (err) {
          setState("listings", id, (s) => ({
            favorited: false,
            favoritesCount: s.favoritesCount - 1
          }));
          throw err;
        }
      }
    },
    async unmakeFavorite(id) {
      const listing = state.listings[id];
      if (listing && listing.favorited) {
        setState("listings", id, (s) => ({
          favorited: false,
          favoritesCount: s.favoritesCount - 1
        }));
        try {
          await agent.Listings.unfavorite(id);
        } catch (err) {
          setState("listings", id, (s) => ({
            favorited: true,
            favoritesCount: s.favoritesCount + 1
          }));
          throw err;
        }
      }
    },
    async createListing(newListing) {
      const { listing, errors } = await agent.Listings.create(newListing);
      if (errors) throw errors;
      setState("listings", { [listing.id]: listing });
      return listing;
    },
    async updateListing(data) {
      const { listing, errors } = await agent.Listings.update(data);
      if (errors) throw errors;
      setState("listings", { [listing.id]: listing });
      return listing;
    },
    async deleteListing(id) {
      const listing = state.listings[id];
      setState("listings", { [id]: undefined });
      try {
        await agent.Listings.del(id);
      } catch (err) {
        setState("listings", { [idp]: listing });
        throw err;
      }
    }
  });
  return listings;
}
