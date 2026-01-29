
# VIDEO DE EXPLICACION: 
https://especialidadesmedicaspe-my.sharepoint.com/:v:/g/personal/nickolas_aguirre_especialidadesmedicaspe_onmicrosoft_com/IQAox95vEWN8RZ57qqCStx5jAQir1u8Q_YqqL65gsiiXRUw?e=wqYkDx
📌 Características principales

✅ Agregar productos configurables al carrito

✅ Manejo de grupos obligatorios y opcionales

✅ Validaciones por reglas de negocio (EQUAL_THAN, LOWER_EQUAL_THAN)

✅ Actualización de cantidades de productos y atributos

✅ Eliminación automática de atributos y grupos opcionales vacíos

✅ Recalculo automático de precio final

❌ Manejo explícito de errores de validación

Ejemplo de Pruebas para cada metodo:

1. CREAR CARRITO (AddProduct)
Endpoint: POST /api/shoppingcart/AddProduct

```json
{
  "productId": 3546345,
  "quantity": 2,
  "groupAttribute": [
    {
      "groupAttributeId": 241887,
      "attributes": [
        {
          "attributeId": 968636,
          "quantity": 1
        }
      ]
    },
    {
      "groupAttributeId": 241888,
      "attributes": [
        {
          "attributeId": 968639,
          "quantity": 2
        },
        {
          "attributeId": 968643,
          "quantity": 1
        }
      ]
    },
    {
      "groupAttributeId": 241889,
      "attributes": [
        {
          "attributeId": 968646,
          "quantity": 1
        },
        {
          "attributeId": 968647,
          "quantity": 1
        },
        {
          "attributeId": 968648,
          "quantity": 1
        },
        {
          "attributeId": 968649,
          "quantity": 1
        }
      ]
    },
    {
      "groupAttributeId": 241890,
      "attributes": [
        {
          "attributeId": 968655,
          "quantity": 1
        },
        {
          "attributeId": 968656,
          "quantity": 1
        }
      ]
    },
    {
      "groupAttributeId": 241891,
      "attributes": [
        {
          "attributeId": 968663,
          "quantity": 1
        }
      ]
    },
    {
      "groupAttributeId": 241892,
      "attributes": [
        {
          "attributeId": 968670,
          "quantity": 1
        }
      ]
    }
  ]
}


2.- Agregar producto al mismo carrito:
Endpoint: POST /api/shoppingcart/AddProduct?cartId={cartId}

{
  "productId": 3546345,
  "quantity": 1,
  "groupAttribute": [
    {
      "groupAttributeId": 241887,
      "attributes": [
        {
          "attributeId": 968637,
          "quantity": 1
        }
      ]
    },
    {
      "groupAttributeId": 241891,
      "attributes": [
        {
          "attributeId": 968664,
          "quantity": 1
        }
      ]
    },
    {
      "groupAttributeId": 241892,
      "attributes": [
        {
          "attributeId": 968671,
          "quantity": 1
        }
      ]
    }
  ]
}


3.- Metodo para obtener los productos del carrito:
GET /api/shoppingcart/{cartId}


4.- Metodo para actualizar todo el producto:
Caso 1: Agregar nuevos registros:
Endpoint: PUT /api/shoppingcart/UpdateProduct/{cartId}

{
  "cartProductId": "CART_PRODUCT_ID_CREADO_INICIALMENTE",
  "productId": 3546345,
  "quantity": 3,
  "groupAttribute": [
    {
      "groupAttributeId": 241887,
      "attributes": [
        {
          "attributeId": 968636,
          "quantity": 1
        }
      ]
    },
    {
      "groupAttributeId": 241888,
      "attributes": [
        {
          "attributeId": 968639,
          "quantity": 2
        },
        {
          "attributeId": 968641,
          "quantity": 2
        }
      ]
    },
    {
      "groupAttributeId": 241889,
      "attributes": [
        {
          "attributeId": 968650,
          "quantity": 3
        },
        {
          "attributeId": 968652,
          "quantity": 2
        }
      ]
    },
    {
      "groupAttributeId": 241890,
      "attributes": [
        {
          "attributeId": 968657,
          "quantity": 2
        },
        {
          "attributeId": 968658,
          "quantity": 1
        }
      ]
    },
    {
      "groupAttributeId": 241891,
      "attributes": [
        {
          "attributeId": 968665,
          "quantity": 1
        }
      ]
    },
    {
      "groupAttributeId": 241892,
      "attributes": [
        {
          "attributeId": 968674,
          "quantity": 1
        }
      ]
    }
  ]
}



Caso 2: Eliminar productos registrados:

{
  "cartProductId": "CART_PRODUCT_ID_CREADO_INICIALMENTE",
  "productId": 3546345,
  "quantity": 1,
  "groupAttribute": [
    {
      "groupAttributeId": 241887,
      "attributes": [
        {
          "attributeId": 968637,
          "quantity": 1
        }
      ]
    },
    {
      "groupAttributeId": 241891,
      "attributes": [
        {
          "attributeId": 968663,
          "quantity": 1
        }
      ]
    },
    {
      "groupAttributeId": 241892,
      "attributes": [
        {
          "attributeId": 968670,
          "quantity": 1
        }
      ]
    }
  ]
}


5.- Metodo para actualizar la cantidad del producto principal: 
Endpoint: PUT /api/shoppingcart/UpdateQuantityProduct/{cartId}

{
  "cartProductId": "ID_DEL_PRODUCTO",
  "quantity": 5
}


6.- Metodo para actualizar la cantidad del attributo de un producto no requerido
Caso 1: Aumentar o restar cantidad:
Endpoint: PUT /api/shoppingcart/UpdateQuantityProductAttribute/{cartId}

{
  "cartProductId": ID_DEL_PRODUCTO,
  "cartProductGroupId": ID_DEL_GRUPO_DEL_PRODUCTO,
  "cartAttributeId": ID_DEL_ATRIBUTO,
  "quantity": 3
}


Caso 2: Eliminar el atributo por la cantidad 0:

{
  "cartProductId": "ID_DEL_PRODUCTO",
  "cartProductGroupId": "ID_DEL_GRUPO_DEL_PRODUCTO",
  "cartAttributeId": "ID_DEL_ATRIBUTO",
  "quantity": 0
}


7.- Metodo para eliminar el producto entero:
Endpoint: DELETE api/ShoppingCart/RemoveProduct/{CartProductId}/{CartId}


8.- Metodo para eliminar atributos no requeridos:
Endpoint: DELETE api/ShoppingCart/RemoveProductAttribute/{CartId}


{
  "cartProductId": ID_DEL_PRODUCTO,
  "cartProductGroupId": ID_DEL_GRUPO_DEL_PRODUCTO,
  "cartAttributeId": ID_DEL_ATRIBUTO
}



=========================================================================
Pruebas que fallaran:
Error 1: Al añadir o actualizar ya que faltan campos requeridos:

{
  "productId": 3546345,
  "quantity": 1,
  "groupAttribute": [
    {
      "groupAttributeId": 241887,
      "attributes": [
        {
          "attributeId": 968636,
          "quantity": 1
        }
      ]
    }
  ]
}


Error 2: Más atributos de los permitidos en grupo EQUAL_THAN

{
  "productId": 3546345,
  "quantity": 1,
  "groupAttribute": [
    {
      "groupAttributeId": 241887,
      "attributes": [
        {
          "attributeId": 968636,
          "quantity": 1
        },
        {
          "attributeId": 968637,
          "quantity": 1
        }
      ]
    },
    {
      "groupAttributeId": 241891,
      "attributes": [
        {
          "attributeId": 968663,
          "quantity": 1
        }
      ]
    },
    {
      "groupAttributeId": 241892,
      "attributes": [
        {
          "attributeId": 968670,
          "quantity": 1
        }
      ]
    }
  ]
}


Error 3: Excede cantidad máxima del grupo LOWER_EQUAL_THAN

{
  "productId": 3546345,
  "quantity": 1,
  "groupAttribute": [
    {
      "groupAttributeId": 241887,
      "attributes": [
        {
          "attributeId": 968636,
          "quantity": 1
        }
      ]
    },
    {
      "groupAttributeId": 241888,
      "attributes": [
        {
          "attributeId": 968639,
          "quantity": 2
        },
        {
          "attributeId": 968640,
          "quantity": 2
        },
        {
          "attributeId": 968641,
          "quantity": 2
        }
      ]
    },
    {
      "groupAttributeId": 241891,
      "attributes": [
        {
          "attributeId": 968663,
          "quantity": 1
        }
      ]
    },
    {
      "groupAttributeId": 241892,
      "attributes": [
        {
          "attributeId": 968670,
          "quantity": 1
        }
      ]
    }
  ]
}


Error 4: Excede maxQuantity de atributo individual


{
  "productId": 3546345,
  "quantity": 1,
  "groupAttribute": [
    {
      "groupAttributeId": 241887,
      "attributes": [
        {
          "attributeId": 968636,
          "quantity": 1
        }
      ]
    },
    {
      "groupAttributeId": 241888,
      "attributes": [
        {
          "attributeId": 968639,
          "quantity": 5
        }
      ]
    },
    {
      "groupAttributeId": 241891,
      "attributes": [
        {
          "attributeId": 968663,
          "quantity": 1
        }
      ]
    },
    {
      "groupAttributeId": 241892,
      "attributes": [
        {
          "attributeId": 968670,
          "quantity": 1
        }
      ]
    }
  ]
}


Error 5: Grupo no existe


{
  "productId": 3546345,
  "quantity": 1,
  "groupAttribute": [
    {
      "groupAttributeId": 999999,
      "attributes": [
        {
          "attributeId": 968636,
          "quantity": 1
        }
      ]
    },
    {
      "groupAttributeId": 241891,
      "attributes": [
        {
          "attributeId": 968663,
          "quantity": 1
        }
      ]
    },
    {
      "groupAttributeId": 241892,
      "attributes": [
        {
          "attributeId": 968670,
          "quantity": 1
        }
      ]
    }
  ]
}


Error 6: Atributo no pertenece al grupo

{
  "productId": 3546345,
  "quantity": 1,
  "groupAttribute": [
    {
      "groupAttributeId": 241887,
      "attributes": [
        {
          "attributeId": 968639,
          "quantity": 1
        }
      ]
    },
    {
      "groupAttributeId": 241891,
      "attributes": [
        {
          "attributeId": 968663,
          "quantity": 1
        }
      ]
    },
    {
      "groupAttributeId": 241892,
      "attributes": [
        {
          "attributeId": 968670,
          "quantity": 1
        }
      ]
    }
  ]
}
