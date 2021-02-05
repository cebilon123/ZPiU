const prod = {
	url: {
		API_URL: 'https://zxcbasfdasdgf.azurewebsites.net',
		CONTRACTOR_API_URL: 'https://webapo20210110092719.azurewebsites.net/api'
	}
}
const dev = {
	url: {
		API_URL: 'https://zxcbasfdasdgf.azurewebsites.net',
		CONTRACTOR_API_URL: 'https://webapo20210110092719.azurewebsites.net/api'
	}
}
export const config = process.env.NODE_ENV === 'development' ? dev : prod