//const API_ROOT = "https://api.realworld.io/api";
//const API_ROOT = "https://localhost:1923/api";
const API_ROOT = "http://localhost:1919/api";
//const API_ROOT = "https://localhost:2024";

const encode = encodeURIComponent;

export default function createAgent([state, actions]) {
  async function send(method, url, data, resKey) {
    const headers = {},
      opts = { method, headers };

    // headers['Access-Control-Allow-Origin'] = 'http://localhost:4200';
    // headers['Access-Control-Allow-Credentials'] = true;

    if (data !== undefined) {
      headers["Content-Type"] = "application/json";
      opts.body = JSON.stringify(data);
    }

    if (state.token)
      headers["Authorization"] = `Bearer ${state.token}`;

    try {
      const response = await fetch(API_ROOT + url, opts);
      const json = await response.json();
      return resKey ? json[resKey] : json;
    } catch (err) {
      if (err && err.response && err.response.status === 401) {
        actions.logout();
      }
      return err;
    }
  }

  const Auth = {
    current: () => send("get", "/user", undefined, "user"),
    login: (email, password) => send("post", "/auth/login", { email, password }),
    register: (username, email, password) => send("post", "/auth/register", { username, email, password }),
    save: user => send("put", "/user", { user })
  };

  const Tags = {
    getAll: () => send("get", "/tags", undefined, "tags")
  };

  const limitQS = (limit, p) => `limit=${limit}&offset=${p ? p * limit : 0}`;
  const limitQS2 = (limit, page) => `limit=${limit}&page=${page}`;
  const omitSlug = listing => Object.assign({}, listing, { slug: undefined });

  const Listings = {
    all: (page, limit = 10) => send("get", `/listings?${limitQS(limit, page)}`),
    byAuthor: (author, page) => send("get", `/listings?author=${encode(author)}&${limitQS(5, page)}`),
    byTag: (tag, page, limit = 10) => send("get", `/listings?tag=${encode(tag)}&${limitQS(limit, page)}`),
    del: slug => send("delete", `/listings/${slug}`),
    favorite: slug => send("post", `/listings/${slug}/favorite`),
    favoritedBy: (author, page) => send("get", `/listings?favorited=${encode(author)}&${limitQS(5, page)}`),
    feed: () => send("get", "/listings/feed?limit=10&offset=0"),
    get: slug => send("get", `/listings/${slug}`, undefined, "listing"),
    unfavorite: slug => send("delete", `/listings/${slug}/favorite`),
    update: listing => send("put", `/listings/${listing.slug}`, { listing: omitSlug(listing) }),
    create: listing => send("post", "/listings", { listing }),

    listings1: (breedId, ownerId, provinceId, districtId, neighborhoodId, page, limit = 10) =>
      send("get", `/listings?BreedId=${breedId}&OwnerId=${ownerId}&ProvinceId=${provinceId}&DistrictId=${districtId}&NeighborhoodId=${neighborhoodId}&${limitQS(limit, page)}`),

    getListings: (filter, page, limit = 10) =>
      send("get", `/listings${filter}?${limitQS2(limit, page)}`),
  };

  const Breeds = {
    getAll: () => send("get", "/breeds/all")
  };

  const Categories = {
    getAll: () => send("get", "/categories")
  };

  const Comments = {
    create: (slug, comment) => send("post", `/listings/${slug}/comments`, { comment }),
    delete: (slug, commentId) => send("delete", `/listings/${slug}/comments/${commentId}`),
    forListing: slug => send("get", `/listings/${slug}/comments`, undefined, "comments")
  };

  const Profile = {
    follow: username => send("post", `/profiles/${username}/follow`),
    get: username => send("get", `/profiles/${username}`, undefined, "profile"),
    unfollow: username => send("delete", `/profiles/${username}/follow`)
  };

  const Territory = {
    countries: () => send("get", `/territory/countries`),
    provinces: countryId => send("get", `/territory/provinces?countryId=${countryId}`),
    districts: provinceId => send("get", `/territory/districts?provinceId=${provinceId}`),
    neighborhoods: districtId => send("get", `/territory/neighborhoods?districtId=${districtId}`)
  };

  return {
    Listings,
    Auth,
    Comments,
    Profile,
    Tags,
    Breeds,
    Categories,
    Territory,
  };
}
