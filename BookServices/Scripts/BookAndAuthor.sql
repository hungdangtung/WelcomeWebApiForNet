﻿select B.Title, B.Genre, B.Year, a.Name, b.Price from Authors A, Books B where A.ID = B.AuthorID;