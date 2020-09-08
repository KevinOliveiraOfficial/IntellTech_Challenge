# IntellTech_Challenge API RESTFUL
**Todas as requisições de método POST devem ser enviadas em JSON, todas retornam um JSON;**

**Para listagem de ambos os dados, basta dar um GET nas URLs apresentadas abaixo;**

**Link da API para criação ou listagem de diretórios:**

> /api/directory

Estrutura de dados para criação de Diretórios (método POST):

 - directory_name: *string não-vazio*
 
 *Retorna o directory_guid e directory_name criado*

**Link da API para criação ou listagem de Formas Geométricas:**

> /api/geometric_shape

Estrutura de dados para criação de Formas Geométricas (método POST):

- geometric_shape_directory_guid: *string contendo o guid do diretório*

- geometric_shape_name: *string não-vazio*

- geometric_shape_type: *inteiro com valor **1** ou **2***

- geometric_shape_color: *string não-vazio*

- geometric_shape_pixels: *inteiro diferente de **0***

*Retorna toda a estrutura de dados acima, juntamente com o novo geometric_shape_directory_guid criado*
