

export default () => {
	





	console.log(listings)

	listings
		.then(res => res.json())
		.then(data => setAllCategories(data))
		.then(() => loadSpecies())
		.then(() => loadBreeds())

	const territoryClient = new TerritoryClient()
	territoryClient.getProvinces(1)
		.then(data => setProvinces(data))
		.then(() => loadDistricts())
		.then(() => loadNeighborhoods())

	const loadDistricts = () => {
		console.log("loadDistricts")
		if (!provinceId()) {
			setSearchParams({ district: "", neighborhood: "" })
			setDistrictId("")
			setDistricts([])
			setNeighborhoodId("")
			setNeighborhoods([])
			return
		}

		console.log("ilçeler getiriliyor")
		territoryClient.getDistricts(provinceId())
			.then(data => setDistricts(data))
	}

	const loadNeighborhoods = () => {
		console.log("districtId", districtId())
		if (!districtId()) {
			setSearchParams({ neighborhood: "" })
			setNeighborhoodId("")
			setNeighborhoods([])
			return
		}

		console.log("mahalleler getiriliyor")
		territoryClient.getNeighborhoods(districtId())
			.then(data => setNeighborhoods(data))
	}

	return (
		<div class="row row-cols-md-6 g-2 align-items-center patiyuva-listing-horizontal-filter">
			



			<div class="col-12">
				<label class="form-label">İlçe</label>
				<select class="form-select" name="district" value={districtId()} onChange={(e) => { handleChange(e).then(id => { setDistrictId(id); loadNeighborhoods() }) }} disabled={districts().length === 0}>
					<option value="">-- Tümü --</option>
					<For each={districts()}>
						{(item) =>
							<option value={item.id} selected={item.id == districtId()}>{item.name}</option>
						}
					</For>
				</select>
			</div>

			<div class="col-12">
				<label class="form-label">Mahalle</label>
				<select class="form-select" name="neighborhood" value={neighborhoodId()} onChange={handleChange} disabled={neighborhoods().length === 0}>
					<option value="">-- Tümü --</option>
					<For each={neighborhoods()}>
						{(item) =>
							<option value={item.id} selected={item.id == neighborhoodId()}>{item.name}</option>
						}
					</For>
				</select>
			</div>

			<div class="col-12">
				<label class="form-label">Aranacak metin</label>
				<input type="text" maxLength={20} class="form-control" name="searchterm" value={searchterm()} onChange={handleChange} />
			</div>

		</div>
	);
};
