var app = app || {};
app.blogController = Vue.createApp({
    data() {
        return {
            blogViewModel: {comments: {replies: []}},
            formData: {
                blogId: null,
                commentId: null,
                name: null,
                email: null,
                message: null,
                fileUpload: null,
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
        this.initializeViewModel();
        this.toast = new bootstrap.Toast('#form-toast');
    },
    methods: {
        initializeViewModel() {
            this.blogViewModel = blogViewModel;
            this.reply.blogId = this.blogViewModel.id;
            this.formData.blogId = this.blogViewModel.id;
            this.originalFormData = this.deepClone(this.formData);
            this.originalReply = this.deepClone(this.reply);
        },

        setupReply(id) {
            if (this.reply.commentId) {
                this.toggleCollapse(`#collapse-${this.reply.commentId}`, false);
            }
            this.resetReplyData();
            this.reply.commentId = id;
            this.toggleCollapse(`#collapse-${id}`, true);
        },

        async sendReply() {
            if (!this.reply.message || !this.reply.commentId || !this.reply.blogId) {
                return this.showToast("Reply Failed!", "Please try again");
            }
            const res = await fetch('/BlogApi/ReplyToComment/', {
                method: 'POST',
                headers: {'Content-Type': 'application/json'},
                body: JSON.stringify(this.reply)
            }).catch(res => console.log(res));

            this.blogViewModel = await res.json();
            this.toggleCollapse(`#collapse-${this.reply.commentId}`, false);
            this.resetReplyData();
        },

        async submitComment(event) {
            event.preventDefault();
            console.log(this.formData);
            console.log(this.mapFormData());

            const res = await fetch('/BlogApi/SubmitComment/', {
                method: 'POST',
                body: this.mapFormData()
            }).catch(res => console.log(res));

            this.blogViewModel = await res.json();
            this.showToast(`Thank you for your comment, ${this.formData.name}!`,
                "Check the bottom of this page to see it appear dynamically.");
            this.resetFormData();
        },

        mapFormData() {
            const formData = new FormData();

            formData.append("blogId", this.formData.blogId);
            formData.append("commentId", this.formData.commentId);
            formData.append("name", this.formData.name);
            formData.append("email", this.formData.email);
            formData.append("message", this.formData.message);

            if (this.formData.fileUpload) {
                formData.append("fileUpload", this.formData.fileUpload);
            }

            return formData;
        },

        showToast(header, body) {
            this.toastHeader = header;
            this.toastMessage = body;
            this.toast.show();
        },

        toAvatarUrl(name) {
            const avatarName = name ? name.trim().split(/\s+/).join('+') : 'A+B';
            return `https://eu.ui-avatars.com/api/?name=${avatarName}`;
        },

        resetFormData() {
            this.formData = this.deepClone(this.originalFormData);
        },

        resetReplyData() {
            this.reply = this.deepClone(this.originalReply);
        },

        deepClone(obj) {
            return JSON.parse(JSON.stringify(obj));
        },

        handleFileChange(event) {
            this.formData.fileUpload = event.target.files[0];
        },

        toggleCollapse(selector, show) {
            const collapseElement = new bootstrap.Collapse(selector);
            if (show) {
                collapseElement.show();
            } else {
                collapseElement.hide();
            }
        }
    }
}).mount("#blog-js");
