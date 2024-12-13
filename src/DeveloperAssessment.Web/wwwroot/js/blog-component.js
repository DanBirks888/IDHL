var app = app || {};
app.contactUsController = Vue.createApp({
    data() {
        return {
            blogViewModel: {
                comments: {
                    replies: [],
                }
            },
            formData: {
                blogId: null,
                commentId: null,
                name: null,
                email: null,
                message: null,
            },
            reply: {
                blogId: null,
                commentId: null,
                message: null,
            },
            replyingTo: '',
            toast: null,
            toastHeader: null,
            toastMessage: null,
        }
    },
    mounted() {
        let self = this;
        self.blogViewModel = blogViewModel;
        self.reply.blogId = self.blogViewModel.id;
        self.formData.blogId = self.blogViewModel.id;
        self.originalFormData = JSON.parse(JSON.stringify(self.formData));
        self.originalReply = JSON.parse(JSON.stringify(self.reply));
        self.toast = new bootstrap.Toast('#form-toast');
    },
    methods: {
        setupReply(id) {
            let self = this;
            if (self.reply.commentId) {
                new bootstrap.Collapse(`#collapse-${self.reply.commentId}`);
            }
            self.resetReplyData();
            self.reply.commentId = id;
            new bootstrap.Collapse(`#collapse-${id}`).show();
        },
        async sendReply(id) {
            let self = this;
            if (!self.reply.message || !self.reply.commentId || !self.reply.blogId) {
                self.showToast("Reply Failed!", "Please try again");
            }
            self.newBlogModel = await self.callApi('/BlogApi/ReplyToComment/', self.reply);
            self.blogViewModel = self.newBlogModel;
            new bootstrap.Collapse(`#collapse-${self.reply.commentId}`);
            self.resetReplyData();
        },
        async sendForm(event) {
            let self = this;
            event.preventDefault();
            self.blogViewModel = await self.callApi('/BlogApi/SubmitComment/', self.formData);
            self.showToast(`Thank you for your comment ${self.formData.name}!`,
                "Check the bottom of this page to see it appear dynamically.")
            self.resetFormData();
        },
        async callApi(url, bodyData) {
            const res = await fetch(url, {
                method: 'POST',
                headers: {'Content-Type': 'application/json'},
                body: JSON.stringify(bodyData)
            }).catch(error => console.log(error));
            return await res.json();
        },
        showToast(messageHeader, messageBody) {
            let self = this;
            self.toastHeader = messageHeader;
            self.toastMessage = messageBody;
            self.toast.show();
        },
        toAvatarUrl(name) {
            return name == null
                ? 'https://eu.ui-avatars.com/api/?name=A+B'
                : `https://eu.ui-avatars.com/api/?name= + ${name.trim().split(/\s+/).join('+')}`;
        },
        resetFormData() {
            let self = this;
            self.formData = JSON.parse(JSON.stringify(self.originalFormData));
        },
        resetReplyData() {
            let self = this;
            self.reply = JSON.parse(JSON.stringify(self.originalReply));
        }
    }
}).mount("#blog-js");