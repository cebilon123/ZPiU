import React, { Component, ChangeEvent } from 'react'
import { Col, Row } from 'react-bootstrap'
import { config } from '../Constants'
import Categories, { ICategory } from './Categories'
import ProductList from './ProductList'

interface IHomeState {
	categories: ICategory[]
}

class Home extends Component<{}, IHomeState> {

	constructor(props: Readonly<{}>) {
		super(props)

		this.state = {
			categories: []
		}

		this.onCategoriesChanged = this.onCategoriesChanged.bind(this)
	}

	onCategoriesChanged(categories: ICategory[]) {
		this.setState({ categories })
	}

	render() {
		return (
			<Row>
				<Col xs={3}>
					<Categories onCategoriesChanged={this.onCategoriesChanged}></Categories>
				</Col>
				<Col>
					<ProductList categories={this.state.categories}></ProductList>
				</Col>
			</Row>
		)
	}
}
export default Home