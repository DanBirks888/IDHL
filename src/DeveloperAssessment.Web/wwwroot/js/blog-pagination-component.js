var app = app || {};
app.blogPaginationComponent = Vue.createApp({
    data() {
        return {
            pagedBlogs: null,
            pageIndex: 1,
            pageSize: 3,
            totalPages: 1,
            totalResults: 0,
            hasPreviousPage: true,
            hasNextPage: true,
            itemsPerPageOptions: [3, 5, 10],
        }
    },
    mounted() {
        this.getPaginatedResults();
    },
    watch: {
        pageIndex: {
            handler() {
                if (this.pageIndex > 0 && this.pageIndex <= this.totalPages) {
                    this.getPaginatedResults();
                }
            }
        },
        pageSize: {
            handler() {
                if (this.pageIndex > 0 && this.pageIndex <= this.totalPages) {
                    this.getPaginatedResults();
                }
            }
        },
    },
    methods: {
        async getPaginatedResults() {
            const res =
                await fetch(`/BlogApi/GetPaginatedBlogs/?pageIndex=${this.pageIndex}&pageSize=${this.pageSize}`
                ).catch(res => console.log(res));
            let response = await res.json();
            this.pagedBlogs = response.items;
            this.pageIndex = response.pageIndex;
            this.totalPages = response.totalPages;
            this.currentPage = response.currentPage;
        },
    }
}).mount("#blog-listing-js");
