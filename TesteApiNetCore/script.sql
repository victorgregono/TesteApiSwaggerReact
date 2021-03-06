--USE [ESTUDO]
GO
/****** Object:  Table [dbo].[Cliente]    Script Date: 16/11/2020 21:18:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cliente](
    [IdClienteCpf] [bigint] NOT NULL,
    [NomeCliente] [varchar](60) NOT NULL,
    [Email] [varchar](60) NULL,
 CONSTRAINT [PK_Cliente] PRIMARY KEY CLUSTERED 
(
    [IdClienteCpf] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

 

GO
/****** Object:  Table [dbo].[ItensPedidos]    Script Date: 16/11/2020 21:18:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ItensPedidos](
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [NumPedido] [int] NOT NULL,
    [IdProduto] [int] NOT NULL,
 CONSTRAINT [PK_ItensPedidos] PRIMARY KEY CLUSTERED 
(
    [Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

 

GO
/****** Object:  Table [dbo].[Pedido]    Script Date: 16/11/2020 21:18:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pedido](
    [NumPedido] [int] IDENTITY(1,1) NOT NULL,
    [DataPedido] [datetime] NOT NULL,
    [NomeCliente] [varchar](60) NOT NULL,
    [ValorPedido] [money] NOT NULL,
    [IdClienteCpf] [bigint] NULL,
 CONSTRAINT [PK_Pedido] PRIMARY KEY CLUSTERED 
(
    [NumPedido] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
 

GO
/****** Object:  Table [dbo].[Produto]    Script Date: 16/11/2020 21:18:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Produto](
    [IdProduto] [int] IDENTITY(1,1) NOT NULL,
    [NomeProduto] [varchar](100) NOT NULL,
    [ValorProduto] [money] NOT NULL,
 CONSTRAINT [PK__Produto__2E883C23C748AADC] PRIMARY KEY CLUSTERED 
(
    [IdProduto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

 

GO
ALTER TABLE [dbo].[Pedido] ADD  DEFAULT (getdate()) FOR [DataPedido]
GO


----------- carga de dados --------------------

--USE [ESTUDO]
GO
INSERT [dbo].[Cliente] ([IdClienteCpf], [NomeCliente], [Email]) VALUES (9876555, N'carlos victor', N'teste@teste.com')
GO
INSERT [dbo].[Cliente] ([IdClienteCpf], [NomeCliente], [Email]) VALUES (123456789, N'guilherme', N'teste@hoje.com.br')
GO
INSERT [dbo].[Cliente] ([IdClienteCpf], [NomeCliente], [Email]) VALUES (97219207387, N'victor grego', N'teste@grego.com')
GO
SET IDENTITY_INSERT [dbo].[ItensPedidos] ON 
GO
INSERT [dbo].[ItensPedidos] ([Id], [NumPedido], [IdProduto]) VALUES (3, 2, 3)
GO
INSERT [dbo].[ItensPedidos] ([Id], [NumPedido], [IdProduto]) VALUES (4, 3, 2)
GO
INSERT [dbo].[ItensPedidos] ([Id], [NumPedido], [IdProduto]) VALUES (5, 3, 3)
GO
INSERT [dbo].[ItensPedidos] ([Id], [NumPedido], [IdProduto]) VALUES (7, 4, 3)
GO
SET IDENTITY_INSERT [dbo].[ItensPedidos] OFF
GO

INSERT [dbo].[Pedido] ( [NomeCliente], [ValorPedido], [IdClienteCpf]) VALUES ('victor grego', 55.0000, 97219207387)
GO
INSERT [dbo].[Pedido] ( [NomeCliente], [ValorPedido], [IdClienteCpf]) VALUES ('latrelteste1', 100.0000, 123456)
GO
INSERT [dbo].[Pedido] ( [NomeCliente], [ValorPedido], [IdClienteCpf]) VALUES ('teste2', 10.0000, 123)

GO
INSERT [dbo].[Produto] ( [NomeProduto], [ValorProduto]) VALUES ('telefone', 55.4500)
GO
INSERT [dbo].[Produto] ( [NomeProduto], [ValorProduto]) VALUES ('cadeira', 100.0000)
GO
INSERT [dbo].[Produto] ([NomeProduto], [ValorProduto]) VALUES ('ventilador', 50.0000)




