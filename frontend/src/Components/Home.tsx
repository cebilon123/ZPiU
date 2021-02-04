import React, { Component, ChangeEvent } from 'react'
import { Button, Card, Col, Form, InputGroup, ListGroup, Row, Table } from 'react-bootstrap'
import { config } from '../Constants'
import Categories, { ICategory } from './Categories'
import { IContractorPrice } from './ProductEditor'
import ProductList from './ProductList'

interface IHomeState {
	categories: ICategory[],
	contractorNIP: number,
	contractorPrices: IContractorPrice[]
	foundContractor?: { KontrahentId: number, Nazwa: string, Numer_Nip: string } | null
}

class Home extends Component<{}, IHomeState> {
	private contractorUrlBase = config.url.CONTRACTOR_API_URL

	constructor(props: Readonly<{}>) {
		super(props)

		this.state = {
			categories: [],
			contractorNIP: 0,
			contractorPrices: [],
			foundContractor: undefined
		}

		this.onCategoriesChanged = this.onCategoriesChanged.bind(this)
		this.handleChange = this.handleChange.bind(this)
		this.handleSearch = this.handleSearch.bind(this)
		this.handleGoHome = this.handleGoHome.bind(this)
	}

	onCategoriesChanged(categories: ICategory[]) {
		this.setState({ categories })
	}

	handleChange(event: any) {
		this.setState({contractorNIP: parseInt(event.target.value)})
	}

	handleGoHome() {
		this.setState({foundContractor: undefined})
	}

	handleSearch() {
		fetch(this.contractorUrlBase+`/kontrahenci/${this.state.contractorNIP}`)
			.then(async res => {
				if (res.status !== 200) {
					this.setState({foundContractor: null})
					return
				}

				res.json().then((contractor) => {
					this.setState({foundContractor: contractor})
				}).catch(() => { // WTF
					this.setState({foundContractor: null})
				})
			})
	}

	render() {
		return (
			<Row>
				<Col xs={3}>
					<Categories onCategoriesChanged={this.onCategoriesChanged}></Categories>
					<Card className="mt-2">
						<Card.Header>Contractor prices</Card.Header>
						<ListGroup variant="flush">						
							<ListGroup.Item>
								<InputGroup>
									<Form.Control type="text" placeholder="NIP" onChange={this.handleChange}/>
									<InputGroup.Append>
										<Button onClick={this.handleSearch}>Search</Button>
									</InputGroup.Append>
								</InputGroup>
							</ListGroup.Item>
						</ListGroup>
					</Card>
				</Col>
				<Col>
					{this.state.foundContractor === null ? 
						<>Contractor not found <Button className="ml-2" onClick={this.handleGoHome}>Back</Button></>
					: this.state.foundContractor !== undefined ?
					  <ProductList contractor={this.state.foundContractor} categories={this.state.categories} goHome={this.handleGoHome}></ProductList>
					: <ProductList categories={this.state.categories}></ProductList>
					}
					
				</Col>
			</Row>
		)
	}
}
export default Home