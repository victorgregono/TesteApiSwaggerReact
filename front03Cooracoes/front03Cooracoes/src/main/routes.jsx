import React from 'react'
import { Switch, Route, Redirect } from 'react-router'

import Home from '../components/home/home'
import UserCrud from '../components/user/user-crud'
import PedidoCrud from '../components/pedido/pedido-crud'
import ProdutoCrud from '../components/produto/produto-crud'

/*Mapeamento dos links aos componentes*/
export default props =>
    <Switch>
        <Route exact path="/" component={Home} />
        <Route exact path="/users" component={UserCrud} />
        <Route exact path="/pedidos" component={PedidoCrud} />
        <Route exact path="/produtos" component={ProdutoCrud} />
        <Redirect from="*" to="/" />
    </Switch>


